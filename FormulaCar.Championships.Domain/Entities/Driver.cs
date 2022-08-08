namespace FormulaCar.Championships.Domain.Entities;

public class Driver
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public MediaTag? MediaTag { get; set; }
    public int CountryId { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}