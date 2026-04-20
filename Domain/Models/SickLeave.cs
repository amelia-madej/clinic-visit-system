using Domain.Models;

namespace Domain;

public class SickLeave
{
    public int Id { get; set; }

    public int VisitId { get; set; }
    public Visit Visit { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Reason { get; set; }
}
