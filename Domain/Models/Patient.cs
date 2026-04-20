namespace Domain.Models;

public class Patient
{
    public int PatientId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string PESEL { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public List<Visit>? Visits { get; set; }
}
