namespace FormulaCar.Championships.Domain.Entities;

public class ScorePointRegulation
{
    public int Id { get; set; }
    public int Point { get; set; }
    public RegulationRule RegulationRule { get; set; }
    public Position Position { get; set; }
}