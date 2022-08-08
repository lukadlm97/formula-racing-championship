namespace FormulaCar.Championships.Domain.Entities;

public class RacePitStop : Result
{
    public int Count { get; set; }
    public TimeSpan TotalTime { get; set; }
}