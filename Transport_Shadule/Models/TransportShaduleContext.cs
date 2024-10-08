using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Transport_Shadule.Models;

public partial class TransportShaduleContext : DbContext
{
    public TransportShaduleContext()
    {
    }

    public TransportShaduleContext(DbContextOptions<TransportShaduleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hr> Hrs { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RouteStaff> RouteStaffs { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleStopsView> ScheduleStopsViews { get; set; }

    public virtual DbSet<Stop> Stops { get; set; }

    public virtual DbSet<StopRoute> StopRoutes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=db8225.public.databaseasp.net; Database=db8225; User Id=db8225; Password=Ze9%8F_cx@3K; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hr>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__HR__7AD04FF1BE4C722E");

            entity.ToTable("HR");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Position).HasMaxLength(100);
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Routes__80979AADC395437D");

            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.Distance).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RouteName).HasMaxLength(255);
            entity.Property(e => e.VehicleType).HasMaxLength(50);
        });

        modelBuilder.Entity<RouteStaff>(entity =>
        {
            entity.HasKey(e => e.RouteStaffId).HasName("PK__RouteSta__C5A247FEB2CE6547");

            entity.ToTable("RouteStaff");

            entity.Property(e => e.RouteStaffId).HasColumnName("RouteStaffID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.Shift).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.RouteStaffs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteStaf__Emplo__619B8048");

            entity.HasOne(d => d.Route).WithMany(p => p.RouteStaffs)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteStaf__Route__60A75C0F");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B69497A6ED9");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.DayOfWeek).HasMaxLength(50);
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.StopId).HasColumnName("StopID");

            entity.HasOne(d => d.Route).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__RouteI__5AEE82B9");

            entity.HasOne(d => d.Stop).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.StopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__StopID__5BE2A6F2");
        });

        modelBuilder.Entity<ScheduleStopsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ScheduleStopsView");

            entity.Property(e => e.DayOfWeek).HasMaxLength(50);
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.StopId).HasColumnName("StopID");
            entity.Property(e => e.StopName).HasMaxLength(255);
        });

        modelBuilder.Entity<Stop>(entity =>
        {
            entity.HasKey(e => e.StopId).HasName("PK__Stops__EB6A38D49AEC6319");

            entity.Property(e => e.StopId).HasColumnName("StopID");
            entity.Property(e => e.StopName).HasMaxLength(255);
        });

        modelBuilder.Entity<StopRoute>(entity =>
        {
            entity.HasKey(e => e.StopRouteId).HasName("PK__StopRout__96E67E64CA24D111");

            entity.ToTable("StopRoute");

            entity.Property(e => e.StopRouteId).HasColumnName("StopRouteID");
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.StopId).HasColumnName("StopID");

            entity.HasOne(d => d.Route).WithMany(p => p.StopRoutes)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StopRoute__Route__5812160E");

            entity.HasOne(d => d.Stop).WithMany(p => p.StopRoutes)
                .HasForeignKey(d => d.StopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StopRoute__StopI__571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
