namespace FormulaCar.Championships.Domain.Entities;

public class RaceSectorTime : Result
{
    public TimeSpan Time { get; set; }
    public Sector Sector { get; set; }
}