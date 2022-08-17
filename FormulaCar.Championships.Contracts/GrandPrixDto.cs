namespace FormulaCar.Championships.Contracts;

public class GrandPrixDto
{
    public int Id { get; set; }
    public int No { get; set; }
    public string GrandPrixName { get; set; }
    public string Season { get; set; }
    public bool SprintRace { get; set; }
}