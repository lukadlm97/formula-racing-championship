namespace FormulaCar.Championships.Domain.Entities;

public class Raceweek
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public bool IsContainsSprintQualification { get; set; }
    public Circuite Circuite { get; set; }
    public int SeasonId { get; set; }
    public ICollection<Result> Results { get; set; }
}