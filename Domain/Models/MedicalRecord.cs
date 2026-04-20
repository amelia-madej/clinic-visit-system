using Domain.Models;

namespace Domain;

public class MedicalRecord
{
    public int Id { get; set; }

    public int VisitId { get; set; }
    public Visit Visit { get; set; }

    public string Interview { get; set; }
    public string Diagnosis { get; set; }
    public string Recommendations { get; set; }

    public List<Prescription> Prescriptions  { get; set; } = new();
}
