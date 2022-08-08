namespace FormulaCar.Championships.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int ContactLenght { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public bool IsActive { get; set; }
    
    public int DriverId { get; set; }
    public int ConstructorId { get; set; }
    public int SeasonId { get; set; }
    public ICollection<Result> Results { get; set; }
}