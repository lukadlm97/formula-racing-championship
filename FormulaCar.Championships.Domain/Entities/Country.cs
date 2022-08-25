namespace FormulaCar.Championships.Domain.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public MediaTag? MediaTag { get; set; }
    public ICollection<Driver> Drivers { get; set; }
    public ICollection<Constructor> Constructors { get; set; }
    public ICollection<Circuite> Circuites { get; set; }
    public ICollection<Engine> Engines { get; set; }
}