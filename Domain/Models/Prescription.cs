namespace Domain.Models;

public class Prescription
{
    public int PrescriptionId { get; set; }
    public int MedicalRecordId { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
    public List<PrescriptionItem> Items { get; set; } = new();
    public DateTime ValidUntil { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
