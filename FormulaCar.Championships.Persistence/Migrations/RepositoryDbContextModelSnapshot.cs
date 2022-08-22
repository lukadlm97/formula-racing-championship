﻿// <auto-generated />
using System;
using FormulaCar.Championships.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    [DbContext(typeof(RepositoryDbContext))]
    partial class RepositoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ConstructorId")
                        .HasColumnType("int");

                    b.Property<int>("ContactLenght")
                        .HasColumnType("int");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConstructorId");

                    b.HasIndex("DriverId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Bookings", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Circuite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<int?>("MediaTagId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("MediaTagId");

                    b.ToTable("Circuites", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Constructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FirstApperance")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("MediaTagId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("MediaTagId");

                    b.ToTable("Constructors", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MediaTagId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("MediaTagId");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("MediaTagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("MediaTagId");

                    b.ToTable("Drivers", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.MediaTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MediaTags", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("PeriodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortPeriodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("QualificationPeriods", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Raceweek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CircuiteId")
                        .HasColumnType("int");

                    b.Property<bool>("IsContainsSprintQualification")
                        .HasColumnType("bit");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CircuiteId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Raceweeks", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("RaceweekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("PositionId");

                    b.HasIndex("RaceweekId");

                    b.ToTable("Result", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RaceNumber")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Seasons", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("SectorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sectors", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationBestSectorTimes", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasIndex("SectorId");

                    b.ToTable("QualificationBestSectorTimes", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationClassification", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<int>("Laps")
                        .HasColumnType("int");

                    b.Property<int>("QualificationPeriodId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasIndex("QualificationPeriodId");

                    b.ToTable("QualificationClassifications", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationMaximumSpeed", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<double>("MaxAvgSpeed")
                        .HasColumnType("float");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.HasIndex("SectorId");

                    b.ToTable("QualificationMaximumSpeeds", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationSpeedTrap", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<double>("MaxSpeed")
                        .HasColumnType("float");

                    b.ToTable("QualificationSpeedTraps", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceClassification", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<int>("Laps")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.ToTable("RaceClassifications", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceFastesLap", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<double>("AvgSpeed")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("Gap")
                        .HasColumnType("time");

                    b.Property<int>("Lap")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("LapTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("RegistrationTime")
                        .HasColumnType("datetime2");

                    b.ToTable("RaceFastesLaps", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceMaximumSpeed", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<double>("MaxAvgSpeed")
                        .HasColumnType("float");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.HasIndex("SectorId");

                    b.ToTable("RaceMaximumSpeeds", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RacePitStop", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TotalTime")
                        .HasColumnType("time");

                    b.ToTable("RacePitStops", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceSectorTime", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasIndex("SectorId");

                    b.ToTable("RaceSectorTimes", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceSpeedTrap", b =>
                {
                    b.HasBaseType("FormulaCar.Championships.Domain.Entities.Result");

                    b.Property<double>("MaxSpeed")
                        .HasColumnType("float");

                    b.ToTable("RaceSpeedTraps", (string)null);
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Booking", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Constructor", null)
                        .WithMany("Bookings")
                        .HasForeignKey("ConstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Driver", null)
                        .WithMany("Bookings")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Season", null)
                        .WithMany("Bookings")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Circuite", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Country", null)
                        .WithMany("Circuites")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.MediaTag", "MediaTag")
                        .WithMany()
                        .HasForeignKey("MediaTagId");

                    b.Navigation("MediaTag");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Constructor", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Country", null)
                        .WithMany("Constructors")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.MediaTag", "MediaTag")
                        .WithMany()
                        .HasForeignKey("MediaTagId");

                    b.Navigation("MediaTag");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Country", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.MediaTag", "MediaTag")
                        .WithMany()
                        .HasForeignKey("MediaTagId");

                    b.Navigation("MediaTag");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Driver", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Country", null)
                        .WithMany("Drivers")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.MediaTag", "MediaTag")
                        .WithMany()
                        .HasForeignKey("MediaTagId");

                    b.Navigation("MediaTag");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Raceweek", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Circuite", "Circuite")
                        .WithMany()
                        .HasForeignKey("CircuiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Season", null)
                        .WithMany("Raceweeks")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Circuite");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Result", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Booking", null)
                        .WithMany("Results")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Position", "Position")
                        .WithMany("Results")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Raceweek", null)
                        .WithMany("Results")
                        .HasForeignKey("RaceweekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationBestSectorTimes", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.QualificationBestSectorTimes", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Sector", null)
                        .WithMany("QualificationBestSectorTimes")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationClassification", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.QualificationClassification", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.QualificationPeriod", null)
                        .WithMany("QualificationClassifications")
                        .HasForeignKey("QualificationPeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationMaximumSpeed", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.QualificationMaximumSpeed", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Sector", null)
                        .WithMany("QualificationMaximumSpeeds")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationSpeedTrap", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.QualificationSpeedTrap", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceClassification", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.RaceClassification", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceFastesLap", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.RaceFastesLap", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceMaximumSpeed", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.RaceMaximumSpeed", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Sector", null)
                        .WithMany("RaceMaximumSpeeds")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RacePitStop", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.RacePitStop", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceSectorTime", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.RaceSectorTime", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("FormulaCar.Championships.Domain.Entities.Sector", null)
                        .WithMany("RaceSectorTimes")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.RaceSpeedTrap", b =>
                {
                    b.HasOne("FormulaCar.Championships.Domain.Entities.Result", null)
                        .WithOne()
                        .HasForeignKey("FormulaCar.Championships.Domain.Entities.RaceSpeedTrap", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Booking", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Constructor", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Country", b =>
                {
                    b.Navigation("Circuites");

                    b.Navigation("Constructors");

                    b.Navigation("Drivers");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Driver", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Position", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.QualificationPeriod", b =>
                {
                    b.Navigation("QualificationClassifications");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Raceweek", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Season", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Raceweeks");
                });

            modelBuilder.Entity("FormulaCar.Championships.Domain.Entities.Sector", b =>
                {
                    b.Navigation("QualificationBestSectorTimes");

                    b.Navigation("QualificationMaximumSpeeds");

                    b.Navigation("RaceMaximumSpeeds");

                    b.Navigation("RaceSectorTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
