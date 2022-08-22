namespace FormulaCar.Championships.Domain.Entities;

public class Sector
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string SectorName { get; set; }
    public ICollection<RaceSectorTime> RaceSectorTimes { get; set; }
    public ICollection<RaceMaximumSpeed> RaceMaximumSpeeds { get; set; }
    public ICollection<QualificationBestSectorTimes> QualificationBestSectorTimes { get; set; }
    public ICollection<QualificationMaximumSpeed> QualificationMaximumSpeeds { get; set; }
}