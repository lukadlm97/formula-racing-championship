namespace FormulaCar.Championships.Domain.Entities;

public class RaceMaximumSpeed : Result
{
    public int MaxAvgSpeed { get; set; }
    public Sector Sector { get; set; }
}