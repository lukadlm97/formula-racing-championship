namespace FormulaCar.Championships.Domain.Entities;

public class Circuite
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public double Length { get; set; }
    public int Capacity { get; set; }
    public MediaTag? MediaTag { get; set; }
    public int? Turns { get; set; }
    public int? DrsZone { get; set; }

    public int CountryId { get; set; }
}