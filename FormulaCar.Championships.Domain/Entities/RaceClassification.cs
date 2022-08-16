namespace FormulaCar.Championships.Domain.Entities;

public class RaceClassification : Result
{
    public int Laps { get; set; }
    public TimeSpan Time { get; set; }
}