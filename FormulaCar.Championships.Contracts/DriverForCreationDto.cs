namespace FormulaCar.Championships.Contracts;

public class DriverForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public bool IsActive { get; set; } = true;
}