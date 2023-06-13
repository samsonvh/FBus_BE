using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FBus_BE.Models;

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

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RouteStation> RouteStations { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=FBus_Local:ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC078951F353");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Code, "UQ__Account__A25C5AA74526B0C1").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Account__A9D10534A2EBD7FA").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Bus__3214EC076554D3F5");

            entity.ToTable("Bus");

            entity.HasIndex(e => e.LicensePlate, "UQ__Bus__026BC15CD851FE5F").IsUnique();

            entity.HasIndex(e => e.Code, "UQ__Bus__A25C5AA71E68DB73").IsUnique();

            entity.Property(e => e.Brand).HasMaxLength(20);
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Color).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
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
                .HasConstraintName("FK__Bus__CreatedById__4222D4EF");
        });

        modelBuilder.Entity<BusTrip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BusTrip__3214EC07C68385DA");

            entity.ToTable("BusTrip");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndingDate).HasColumnType("datetime");
            entity.Property(e => e.StartingDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Coordination).WithMany(p => p.BusTrips)
                .HasForeignKey(d => d.CoordinationId)
                .HasConstraintName("FK__BusTrip__Coordin__5BE2A6F2");
        });

        modelBuilder.Entity<BusTripStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BusTripS__3214EC073E2AA64C");

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
                .HasConstraintName("FK__BusTripSt__BusTr__5EBF139D");

            entity.HasOne(d => d.Station).WithMany(p => p.BusTripStatuses)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__BusTripSt__Stati__5FB337D6");
        });

        modelBuilder.Entity<Coordination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coordina__3214EC07551FDC20");

            entity.ToTable("Coordination");

            entity.Property(e => e.DateLine).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Bus).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.BusId)
                .HasConstraintName("FK__Coordinat__BusId__534D60F1");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Coordinat__Creat__5165187F");

            entity.HasOne(d => d.Driver).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Coordinat__Drive__52593CB8");

            entity.HasOne(d => d.Route).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__Coordinat__Route__5441852A");
        });

        modelBuilder.Entity<CoordinationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coordina__3214EC0765AA1720");

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
                .HasConstraintName("FK__Coordinat__Coord__571DF1D5");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CoordinationStatuses)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Coordinat__Drive__5812160E");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Driver__3214EC07F4AD6845");

            entity.ToTable("Driver");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Driver__85FB4E389DBDB4A3").IsUnique();

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
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.DriverAccounts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Driver__AccountI__3C69FB99");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.DriverCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Driver__CreatedB__6E01572D");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Route__3214EC0782C3D46E");

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
                .HasConstraintName("FK__Route__CreatedBy__44FF419A");
        });

        modelBuilder.Entity<RouteStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RouteSta__3214EC076C4097FC");

            entity.ToTable("RouteStation");

            entity.HasOne(d => d.Route).WithMany(p => p.RouteStations)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__RouteStat__Route__4D94879B");

            entity.HasOne(d => d.Station).WithMany(p => p.RouteStations)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__RouteStat__Stati__4E88ABD4");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Station__3214EC07D387691B");

            entity.ToTable("Station");

            entity.HasIndex(e => e.Code, "UQ__Station__A25C5AA778048484").IsUnique();

            entity.Property(e => e.AddressNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
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
                .HasConstraintName("FK__Station__Created__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
