namespace FormulaCar.Championships.Service.Abstraction;

public interface IServiceManager
{
    IPositionService PositionService { get; }
    ISectorService SectorService { get; }
    IQualificationPeriodsService QualificationPeriodsService { get; }
    ICountryService CountryService { get; }
    ICircuiteService CircuiteService { get; }
}