namespace FormulaCar.Championships.Domain.Entities;

public class RaceFastesLap:Result
{
    public TimeSpan LapTime { get; set; }
    public int Lap { get; set; }
    public TimeSpan Gap { get; set; }
    public double AvgSpeed { get; set; }
    public DateTime RegistrationTime { get; set; }
}