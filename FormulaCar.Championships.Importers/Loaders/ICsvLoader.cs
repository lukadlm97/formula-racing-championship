using FormulaCar.Championships.Importers.Utilities;

namespace FormulaCar.Championships.Importers.Loaders;

public interface ICsvLoader
{
    IEnumerable<DriverImportFormat> GetDrivers();
}