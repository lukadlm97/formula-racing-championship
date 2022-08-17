namespace FormulaCar.Championships.Contracts;

public class RaceResultItemForCreationDto
{
    public int Laps { get; set; }
    public TimeSpan Time { get; set; }
    public string Driver { get; set; }
    public string Circuite { get; set; }
    public string Postion { get; set; }
    public int Season { get; set; }
}