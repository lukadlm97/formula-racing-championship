﻿namespace FormulaCar.Championships.Service.Abstraction;

public interface IServiceManager
{
    IPositionService PositionService { get; }
    ISectorService SectorService { get; }
    IQualificationPeriodsService QualificationPeriodsService { get; }
    ICountryService CountryService { get; }
    ICircuiteService CircuiteService { get; }
    IDriverService DriverService { get; }
    IConstructorService ConstructorService { get; }
    IBookingService BookingService { get; }
    IRaceweekService RaceweekService { get; }
    IRaceClassificationService RaceClassificationService { get; }
    IFastestLapService FastestLapService { get; }
    IRaceSpeedTrapService RaceSpeedTrapService { get; }
    IRacePitStopService RacePitStopService { get; }
    IRaceBestSectorService RaceBestSectorService { get; }
    IRaceMaximumSpeedService RaceMaximumSpeedService { get; } 
    IQualificationBestSectorService QualificationBestSectorService { get; }
    IQualificationClassificationService QualificationClassificationService { get; }
    IQualificationMaxSpeedService QualificationMaxSpeedService { get; }
    IQualificationSpeedTrapService QualificationSpeedTrapService { get; }
    IEngineService EngineService { get; }
}