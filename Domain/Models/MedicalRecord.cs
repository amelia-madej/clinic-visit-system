namespace Domain.Models;

public class MedicalRecord
{
    public int MedicalRecordId { get; set; }
    public int VisitId { get; set; }
    public Visit? Visit { get; set; }
    public SickLeave? SickLeave { get; set; }
    public string Interview { get; set; } = null!;
    public string Diagnosis { get; set; } = null!;
    public string Recommendations { get; set; } = null!;
    public List<Prescription> Prescriptions { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
