namespace FormulaCar.Championships.Domain.Entities;

public class Season
{
    public int Id { get; set; }
    public int Year { get; set; }
    public int RaceNumber { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public ICollection<Raceweek> Raceweeks { get; set; }
}