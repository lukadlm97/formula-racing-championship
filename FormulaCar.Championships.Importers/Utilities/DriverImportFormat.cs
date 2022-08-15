using FormulaCar.Championships.Domain.Entities;

namespace FormulaCar.Championships.Importers.Utilities;

public class DriverImportFormat
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; } = false;
    public Country Country { get; set; }
    public override string ToString()
    {
        return FirstName+" "+LastName+" ["+Country.Name+"]"+"   ("+IsActive+")";
    }
}