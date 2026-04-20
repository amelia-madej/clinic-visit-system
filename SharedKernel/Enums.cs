namespace SharedKernel;

public enum VisitStatus
{
    Scheduled,
    Completed,
    Cancelled
}
public enum VisitType
{
    InPerson,
    Telemedicine,
    HomeVisit
}

public enum UserRole
{
    Admin,
    Doctor,
    Patient
}

public enum Gender
{
    Male,
    Female,
    Other
}