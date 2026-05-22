namespace SharedKernel.DTOs
{
    public class AnomalyAlertDto
    {
        public string Category { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public decimal MetricValue { get; set; }
        public decimal ThresholdValue { get; set; }
        public string Severity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class AnomalyDashboardDto
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public int TotalVisits { get; set; }
        public int TotalSickLeaves { get; set; }
        public int TotalPrescriptions { get; set; }
        public int TotalAlerts { get; set; }
        public Dictionary<string, int> AlertsByCategory { get; set; } = new();
        public List<AnomalyAlertDto> Alerts { get; set; } = new();
    }
}
