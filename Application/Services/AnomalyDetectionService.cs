using Domain.Contracts;
using Domain.Models;
using SharedKernel.DTOs;

namespace Application.Services
{
    public class AnomalyDetectionService : IAnomalyDetectionService
    {
        private readonly IClinicUnitOfWork _uow;

        private static readonly string[] ControlledSubstanceKeywords =
        {
            "morphine", "oxycodone", "fentanyl", "tramadol", "codeine",
            "diazepam", "alprazolam", "clonazepam", "lorazepam", "zolpidem"
        };

        private static readonly Dictionary<string, string[]> SpecializationMedicationKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Cardiology"] = new[] { "ator", "statin", "bisoprolol", "ramipril", "amlodipine", "nitro", "cardio" },
            ["Dermatology"] = new[] { "clotrimazole", "hydrocortisone", "retino", "derm", "skin", "ointment" },
            ["Neurology"] = new[] { "pregabalin", "gabapentin", "levetiracetam", "migraine", "neuro" },
            ["Orthopedics"] = new[] { "diclofenac", "ibuprofen", "ketoprofen", "naproxen", "muscle", "bone" },
            ["Pulmonology"] = new[] { "salbutamol", "budesonide", "fluticasone", "montelukast", "asthma" },
            ["Family Medicine"] = new[] { "amoxicillin", "paracetamol", "ibuprofen", "metformin", "omeprazole" }
        };

        public AnomalyDetectionService(IClinicUnitOfWork uow)
        {
            _uow = uow;
        }

        public AnomalyDashboardDto DetectAnomalies(DateTime periodStart, DateTime periodEnd)
        {
            if (periodEnd < periodStart)
                throw new ArgumentException("Period end cannot be earlier than period start.");

            var start = periodStart.Date;
            var end = periodEnd.Date.AddDays(1).AddTicks(-1);

            var visits = _uow.VisitRepository.GetVisitsByDateRange(start, end);
            var sickLeaves = _uow.SickLeaveRepository.GetAllWithDetails()
                .Where(sl => sl.CreatedAt >= start && sl.CreatedAt <= end)
                .Where(sl => sl.MedicalRecord?.Visit?.Doctor?.User is not null)
                .ToList();

            var prescriptions = _uow.PrescriptionRepository.GetAllWithDetails()
                .Where(p => p.CreatedAt >= start && p.CreatedAt <= end)
                .ToList();

            var alerts = new List<AnomalyAlertDto>();

            AddSickLeaveAlerts(visits, sickLeaves, alerts);
            AddPrescriptionAlerts(prescriptions, alerts);
            AddTemporalAlerts(visits, prescriptions, alerts);

            return new AnomalyDashboardDto
            {
                PeriodStart = start,
                PeriodEnd = end.Date,
                TotalVisits = visits.Count,
                TotalSickLeaves = sickLeaves.Count,
                TotalPrescriptions = prescriptions.Count,
                TotalAlerts = alerts.Count,
                AlertsByCategory = alerts
                    .GroupBy(a => a.Category)
                    .ToDictionary(g => g.Key, g => g.Count()),
                Alerts = alerts
                    .OrderByDescending(a => SeverityRank(a.Severity))
                    .ThenBy(a => a.Category)
                    .ToList()
            };
        }

        private static int SeverityRank(string severity) => severity switch
        {
            "High" => 3,
            "Medium" => 2,
            _ => 1
        };

        private static string SeverityForRatio(decimal ratio)
        {
            if (ratio >= 0.65m) return "High";
            if (ratio >= 0.45m) return "Medium";
            return "Low";
        }

        private static bool IsDiagnosisWeak(string diagnosis)
        {
            if (string.IsNullOrWhiteSpace(diagnosis))
                return true;

            var normalized = diagnosis.Trim().ToLowerInvariant();
            return normalized.Length < 6 || normalized is "checkup" or "control" or "observation";
        }

        private void AddSickLeaveAlerts(List<Visit> visits, List<SickLeave> sickLeaves, List<AnomalyAlertDto> alerts)
        {
            var doctorVisitCounts = visits
                .Where(v => v.Doctor?.User is not null)
                .GroupBy(v => v.DoctorId)
                .ToDictionary(g => g.Key, g => g.Count());

            var doctorLeaveGroups = sickLeaves
                .GroupBy(sl => sl.MedicalRecord!.Visit!.Doctor!)
                .Select(g => new
                {
                    Doctor = g.Key,
                    L4Count = g.Count(),
                    AvgDuration = g.Average(sl => Math.Max(1, (sl.EndDate.Date - sl.StartDate.Date).Days + 1)),
                    WeakDiagnosisRate = g.Count(sl => IsDiagnosisWeak(sl.MedicalRecord!.Diagnosis)) / (decimal)g.Count()
                })
                .ToList();

            var populationRatio = doctorLeaveGroups.Count == 0
                ? 0m
                : doctorLeaveGroups.Average(g => g.L4Count / (decimal)Math.Max(1, doctorVisitCounts.GetValueOrDefault(g.Doctor.DoctorId, 0)));

            foreach (var g in doctorLeaveGroups)
            {
                var visitCount = Math.Max(1, doctorVisitCounts.GetValueOrDefault(g.Doctor.DoctorId, 0));
                var l4VisitRatio = g.L4Count / (decimal)visitCount;
                var user = g.Doctor.User!;
                var doctorName = $"Dr. {user.FirstName} {user.LastName}";

                if (g.L4Count >= 8 && l4VisitRatio > Math.Max(0.40m, populationRatio * 1.8m))
                {
                    alerts.Add(new AnomalyAlertDto
                    {
                        Category = "Excessive Sick Leave Volume",
                        EntityType = "Doctor",
                        EntityId = g.Doctor.DoctorId,
                        EntityName = doctorName,
                        MetricValue = Math.Round(l4VisitRatio, 3),
                        ThresholdValue = Math.Round(Math.Max(0.40m, populationRatio * 1.8m), 3),
                        Severity = SeverityForRatio(l4VisitRatio),
                        Description = $"Sick leave to visits ratio = {Math.Round(l4VisitRatio * 100, 1)}% ({g.L4Count}/{visitCount})."
                    });
                }

                if (g.L4Count >= 5 && g.AvgDuration >= 12)
                {
                    alerts.Add(new AnomalyAlertDto
                    {
                        Category = "Long Sick Leave Duration",
                        EntityType = "Doctor",
                        EntityId = g.Doctor.DoctorId,
                        EntityName = doctorName,
                        MetricValue = Math.Round((decimal)g.AvgDuration, 2),
                        ThresholdValue = 12m,
                        Severity = g.AvgDuration >= 16 ? "High" : "Medium",
                        Description = $"Average sick leave duration: {Math.Round(g.AvgDuration, 1)} days."
                    });
                }

                if (g.L4Count >= 5 && g.WeakDiagnosisRate >= 0.25m)
                {
                    alerts.Add(new AnomalyAlertDto
                    {
                        Category = "Weak Sick Leave Diagnosis",
                        EntityType = "Doctor",
                        EntityId = g.Doctor.DoctorId,
                        EntityName = doctorName,
                        MetricValue = Math.Round(g.WeakDiagnosisRate, 3),
                        ThresholdValue = 0.25m,
                        Severity = g.WeakDiagnosisRate >= 0.40m ? "High" : "Medium",
                        Description = $"Share of sick leaves with weak diagnosis: {Math.Round(g.WeakDiagnosisRate * 100, 1)}%."
                    });
                }
            }
        }

        private void AddPrescriptionAlerts(List<Prescription> prescriptions, List<AnomalyAlertDto> alerts)
        {
            var doctorPrescriptions = prescriptions
                .Where(p => p.MedicalRecord?.Visit?.Doctor?.User is not null)
                .SelectMany(p => p.Items.Select(item => new { Prescription = p, Item = item }))
                .Where(x => x.Item.Medication is not null)
                .ToList();

            var byDoctor = doctorPrescriptions
                .GroupBy(x => x.Prescription.MedicalRecord!.Visit!.Doctor!)
                .ToList();

            foreach (var group in byDoctor)
            {
                var doctor = group.Key;
                var doctorName = $"Dr. {doctor.User!.FirstName} {doctor.User.LastName}";
                var totalItems = group.Count();

                if (totalItems == 0)
                    continue;

                var controlledCount = group.Count(x =>
                {
                    var medText = $"{x.Item.Medication!.Name} {x.Item.Medication.ActiveIngredient}".ToLowerInvariant();
                    return ControlledSubstanceKeywords.Any(k => medText.Contains(k));
                });

                var controlledShare = controlledCount / (decimal)totalItems;
                if (controlledCount >= 12 && controlledShare >= 0.30m)
                {
                    alerts.Add(new AnomalyAlertDto
                    {
                        Category = "Controlled Substances",
                        EntityType = "Doctor",
                        EntityId = doctor.DoctorId,
                        EntityName = doctorName,
                        MetricValue = Math.Round(controlledShare, 3),
                        ThresholdValue = 0.30m,
                        Severity = controlledShare >= 0.45m ? "High" : "Medium",
                        Description = $"Controlled substance share: {Math.Round(controlledShare * 100, 1)}% ({controlledCount}/{totalItems})."
                    });
                }

                if (!string.IsNullOrWhiteSpace(doctor.Specialization)
                    && SpecializationMedicationKeywords.TryGetValue(doctor.Specialization, out var expectedKeywords))
                {
                    var outsideCount = group.Count(x =>
                    {
                        var medText = $"{x.Item.Medication!.Name} {x.Item.Medication.ActiveIngredient}".ToLowerInvariant();
                        return !expectedKeywords.Any(k => medText.Contains(k));
                    });

                    var outsideShare = outsideCount / (decimal)totalItems;
                    if (totalItems >= 20 && outsideShare >= 0.70m)
                    {
                        alerts.Add(new AnomalyAlertDto
                        {
                            Category = "Prescriptions Outside Specialty",
                            EntityType = "Doctor",
                            EntityId = doctor.DoctorId,
                            EntityName = doctorName,
                            MetricValue = Math.Round(outsideShare, 3),
                            ThresholdValue = 0.70m,
                            Severity = outsideShare >= 0.85m ? "High" : "Medium",
                            Description = $"Prescriptions outside typical specialty profile: {Math.Round(outsideShare * 100, 1)}%."
                        });
                    }
                }
            }

            var repeatedPerPatient = doctorPrescriptions
                .GroupBy(x => new
                {
                    DoctorId = x.Prescription.MedicalRecord!.Visit!.DoctorId,
                    DoctorName = $"Dr. {x.Prescription.MedicalRecord!.Visit!.Doctor!.User!.FirstName} {x.Prescription.MedicalRecord!.Visit!.Doctor!.User!.LastName}",
                    PatientId = x.Prescription.MedicalRecord!.Visit!.PatientId,
                    MedicationId = x.Item.MedicationId,
                    MedicationName = x.Item.Medication!.Name
                })
                .Where(g => g.Count() >= 6)
                .ToList();

            foreach (var repeated in repeatedPerPatient)
            {
                alerts.Add(new AnomalyAlertDto
                {
                    Category = "Repeated Prescriptions for Same Patient",
                    EntityType = "Doctor",
                    EntityId = repeated.Key.DoctorId,
                    EntityName = repeated.Key.DoctorName,
                    MetricValue = repeated.Count(),
                    ThresholdValue = 6,
                    Severity = repeated.Count() >= 10 ? "High" : "Medium",
                    Description = $"Medication '{repeated.Key.MedicationName}' prescribed to the same patient {repeated.Count()} times."
                });
            }
        }

        private void AddTemporalAlerts(List<Visit> visits, List<Prescription> prescriptions, List<AnomalyAlertDto> alerts)
        {
            var byDoctor = visits
                .Where(v => v.Doctor?.User is not null)
                .GroupBy(v => v.Doctor!)
                .ToList();

            foreach (var group in byDoctor)
            {
                var total = group.Count();
                if (total < 15)
                    continue;

                var edgeCount = group.Count(v => v.VisitDateTime.Day <= 3 || v.VisitDateTime.Day >= 28);
                var edgeShare = edgeCount / (decimal)total;
                if (edgeShare >= 0.55m)
                {
                    var user = group.Key.User!;
                    alerts.Add(new AnomalyAlertDto
                    {
                        Category = "Month Edge Visit Concentration",
                        EntityType = "Doctor",
                        EntityId = group.Key.DoctorId,
                        EntityName = $"Dr. {user.FirstName} {user.LastName}",
                        MetricValue = Math.Round(edgeShare, 3),
                        ThresholdValue = 0.55m,
                        Severity = edgeShare >= 0.70m ? "High" : "Medium",
                        Description = $"{Math.Round(edgeShare * 100, 1)}% of visits are on days 1-3 or 28-31."
                    });
                }
            }

            var orphanedPrescriptions = prescriptions.Count(p => p.MedicalRecord?.Visit is null);
            if (orphanedPrescriptions > 0)
            {
                alerts.Add(new AnomalyAlertDto
                {
                    Category = "Prescriptions Without Visit",
                    EntityType = "System",
                    EntityName = "Global",
                    MetricValue = orphanedPrescriptions,
                    ThresholdValue = 0,
                    Severity = orphanedPrescriptions >= 5 ? "High" : "Medium",
                    Description = $"Detected {orphanedPrescriptions} prescriptions without a linked visit."
                });
            }
        }
    }
}
