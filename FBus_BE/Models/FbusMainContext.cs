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
        => optionsBuilder.UseSqlServer("Name=FBus_Main:ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC071AFB84E7");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Code, "UQ__Account__A25C5AA79A8B7367").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Account__A9D10534BAE14097").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Bus__3214EC071B2475E8");

            entity.ToTable("Bus");

            entity.HasIndex(e => e.LicensePlate, "UQ__Bus__026BC15C5D6D0C27").IsUnique();

            entity.HasIndex(e => e.Code, "UQ__Bus__A25C5AA76D2EBFC1").IsUnique();

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
                .HasConstraintName("FK__Bus__CreatedById__534D60F1");
        });

        modelBuilder.Entity<BusTrip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BusTrip__3214EC07CC81F768");

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
                .HasConstraintName("FK__BusTrip__Coordin__5535A963");
        });

        modelBuilder.Entity<BusTripStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BusTripS__3214EC07FFF0F983");

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
                .HasConstraintName("FK__BusTripSt__BusTr__571DF1D5");

            entity.HasOne(d => d.Station).WithMany(p => p.BusTripStatuses)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__BusTripSt__Stati__59063A47");
        });

        modelBuilder.Entity<Coordination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coordina__3214EC07758E3ED1");

            entity.ToTable("Coordination");

            entity.Property(e => e.DateLine).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Bus).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.BusId)
                .HasConstraintName("FK__Coordinat__BusId__5AEE82B9");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Coordinat__Creat__5CD6CB2B");

            entity.HasOne(d => d.Driver).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Coordinat__Drive__5EBF139D");

            entity.HasOne(d => d.Route).WithMany(p => p.Coordinations)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__Coordinat__Route__60A75C0F");
        });

        modelBuilder.Entity<CoordinationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coordina__3214EC070C87B6B1");

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
                .HasConstraintName("FK__Coordinat__Coord__628FA481");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.CoordinationStatuses)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Coordinat__Drive__5812160E");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Driver__3214EC07FE78734D");

            entity.ToTable("Driver");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Driver__85FB4E38822B7A54").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Avatar)
                .HasMaxLength(300)
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
                .HasConstraintName("FK__Driver__AccountI__656C112C");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.DriverCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Driver__CreatedB__6754599E");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Route__3214EC0788B5BD17");

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
                .HasConstraintName("FK__Route__CreatedBy__693CA210");
        });

        modelBuilder.Entity<RouteStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RouteSta__3214EC071DED3645");

            entity.ToTable("RouteStation");

            entity.HasOne(d => d.Route).WithMany(p => p.RouteStations)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__RouteStat__Route__6B24EA82");

            entity.HasOne(d => d.Station).WithMany(p => p.RouteStations)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__RouteStat__Stati__6D0D32F4");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Station__3214EC07D1431A54");

            entity.ToTable("Station");

            entity.HasIndex(e => e.Code, "UQ__Station__A25C5AA75B4D6826").IsUnique();

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
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.Ward).HasMaxLength(50);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Stations)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__Station__Created__6EF57B66");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
