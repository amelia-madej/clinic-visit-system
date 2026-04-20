namespace Domain;

public class Prescription
{
    public int Id { get; set; }

    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; }

    public List<PrescriptionItem> Items { get; set; } = new();

    public DateTime? ValidUntil { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
