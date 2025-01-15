using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace COC.ModelDB.QUDB;

public partial class QUDBContext : DbContext
{
    public QUDBContext()
    {
    }

    public QUDBContext(DbContextOptions<QUDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discover> Discovers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<MainMenu> MainMenus { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<PhotosVideo> PhotosVideos { get; set; }

    public virtual DbSet<Social> Socials { get; set; }

    public virtual DbSet<SubMenu> SubMenus { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=QUDB;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<Discover>(entity =>
        {
            entity.ToTable("Discover");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(250)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MainMenu>(entity =>
        {
            entity.ToTable("Main Menu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<PhotosVideo>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(50)
                .HasColumnName("ImageURl");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(50)
                .HasColumnName("VideoURL");
        });

        modelBuilder.Entity<Social>(entity =>
        {
            entity.ToTable("Social");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contact).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Twitter).HasMaxLength(250);
        });

        modelBuilder.Entity<SubMenu>(entity =>
        {
            entity.ToTable("Sub Menu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MainMenuId).HasColumnName("MainMenuID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .HasColumnName("URL");

            entity.HasOne(d => d.MainMenu).WithMany(p => p.SubMenus)
                .HasForeignKey(d => d.MainMenuId)
                .HasConstraintName("FK_Sub Menu_Main Menu");
        });

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_1");

            entity.Property(e => e.Expr1).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
