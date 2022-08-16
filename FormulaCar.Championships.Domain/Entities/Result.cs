namespace FormulaCar.Championships.Domain.Entities;

public abstract class Result
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public int RaceweekId { get; set; }
    public Position Position { get; set; }
}