using System.Security.AccessControl;

namespace FormulaCar.Championships.Domain.Entities;

public class RaceSectorTime : Result
{
    public TimeSpan Time { get; set; }
    public int SectorId { get; set; }

}