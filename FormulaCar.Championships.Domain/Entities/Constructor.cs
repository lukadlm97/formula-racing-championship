namespace FormulaCar.Championships.Domain.Entities;

public class Constructor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime FirstApperance { get; set; }
    public bool IsActive { get; set; }
    public MediaTag? MediaTag { get; set; }

    public int CountryId { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}