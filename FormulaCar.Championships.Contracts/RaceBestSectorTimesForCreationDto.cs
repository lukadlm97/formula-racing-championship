namespace FormulaCar.Championships.Contracts;

public class RaceBestSectorTimesForCreationDto
{
    public string Driver { get; set; }
    public string Circuite { get; set; }
    public string Postion { get; set; }
    public int Season { get; set; }
    public int Sector { get; set; }
    public TimeSpan Time { get; set; }
}