namespace FormulaCar.Championships.Contracts;

public class RaceMaximumSpeedDto
{
    public string Position { get; set; }
    public string Driver { get; set; }
    public string Constructor { get; set; }
    public string Circuite { get; set; }
    public double MaxAvgSpeed { get; set; }
    public string Sector { get; set; }
}