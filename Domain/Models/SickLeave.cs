namespace Domain.Models;

public class SickLeave
{
    public int SickLeaveId { get; set; }
    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; } 
}
