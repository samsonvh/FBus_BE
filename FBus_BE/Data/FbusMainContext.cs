using System;
using System.Collections.Generic;
using FBus_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Data;

public partial class FbusMainContext : DbContext
{
    public FbusMainContext()
    {
    }

    public FbusMainContext(DbContextOptions<FbusMainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<BusTrip> BusTrips { get; set; }

    public virtual DbSet<BusTripStatus> BusTripStatuses { get; set; }

    public virtual DbSet<Coordination> Coordinations { get; set; }

    public virtual DbSet<CoordinationStatus> CoordinationStatuses { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Models.Route> Routes { get; set; }

    public virtual DbSet<RouteStation> RouteStations { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6MQ8GIF;Database=FBus_Main;User ID=sa;Password=Thang123;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC0769D62CB2");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Code, "UQ__Account__A25C5AA7935AE7A9").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Account__A9D105340F967CB5").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bus__3214EC071E131F6F");

            entity.ToTable("Bus");

            entity.HasIndex(e => e.LicensePlate, "UQ__Bus__026BC15C485B568B").IsUnique();

            entity.HasIndex(e => e.Code, "UQ__Bus__A25C5AA7B3D2B80C").IsUnique();

            entity.Property(e => e.Brand).HasMaxLength(20);
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Color).HasMaxLength(20);
            entity.Property(e => e.DateOfRegistration).HasColumnType("date");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Model).HasMaxLength(30);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Buses)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Bus__CreatedById__2F10007B");
        });

        modelBuilder.Entity<BusTrip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BusTrip__3214EC0765ACDABD");

            entity.ToTable("BusTrip");

            entity.Property(e => e.EndingDate).HasColumnType("datetime");
            entity.Property(e => e.StartingDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Coordination).WithMany(p => p.BusTrips)
                .HasForeignKey(d => d.CoordinationId)
                .HasConstraintName("FK__BusTrip__Coordin__48CFD27E");
        });

        modelBuilder.Entity<BusTripStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BusTripS__3214EC077F77A4C0");

            entity.ToTable("BusTripStatus");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(100);
            entity.Property(e => e.OriginalStatus)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedStatus)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.BusTrip).WithMany(p => p.BusTripStatuses)
                .HasForeignKey(d => d.BusTripId)
                .HasConstraintName("FK__BusTripSt__BusTr__4BAC3F29");

            entity.HasOne(d => d.Station).WithMany(p => p.BusTripStatuses)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__BusTripSt__Stati__4CA06362");
        });

        modelBuilder.Entity<Coordination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coordina__3214EC075654BACF");

            entity.ToTable("Coordination");

            entity.Property(e => e.DateLine).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Bus).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.BusId)
                .HasConstraintName("FK__Coordinat__BusId__403A8C7D");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Coordinat__Creat__3E52440B");

            entity.HasOne(d => d.Driver).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Coordinat__Drive__3F466844");

            entity.HasOne(d => d.Route).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__Coordinat__Route__412EB0B6");
        });

        modelBuilder.Entity<CoordinationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coordina__3214EC07EC160D1D");

            entity.ToTable("CoordinationStatus");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(100);
            entity.Property(e => e.OriginalStatus)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedStatus)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Coordination).WithMany(p => p.CoordinationStatuses)
                .HasForeignKey(d => d.CoordinationId)
                .HasConstraintName("FK__Coordinat__Coord__440B1D61");

            entity.HasOne(d => d.Driver).WithMany(p => p.CoordinationStatuses)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Coordinat__Drive__44FF419A");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Driver__3214EC0756C6BFF9");

            entity.ToTable("Driver");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Driver__85FB4E38F3985349").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Avatar)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IdCardNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PersonalEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasConversion<string>();

            entity.HasOne(d => d.Account).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Driver__AccountI__29572725");
        });

        modelBuilder.Entity<Models.Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Route__3214EC078D931592");

            entity.ToTable("Route");

            entity.Property(e => e.Beginning).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Destination).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Routes)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Route__CreatedBy__31EC6D26");
        });

        modelBuilder.Entity<RouteStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RouteSta__3214EC07853383FE");

            entity.ToTable("RouteStation");

            entity.HasOne(d => d.Route).WithMany(p => p.RouteStations)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__RouteStat__Route__3A81B327");

            entity.HasOne(d => d.Station).WithMany(p => p.RouteStations)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__RouteStat__Stati__3B75D760");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Station__3214EC0728C718CB");

            entity.ToTable("Station");

            entity.HasIndex(e => e.Code, "UQ__Station__A25C5AA7F9045DB5").IsUnique();

            entity.Property(e => e.AddressNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.Ward).HasMaxLength(50);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Stations)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Station__Created__36B12243");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
