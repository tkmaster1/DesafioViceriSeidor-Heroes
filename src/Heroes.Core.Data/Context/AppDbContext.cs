using Heroes.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Core.Data.Context;

public class AppDbContext : DbContext
{
    #region Constructor

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    #endregion

    #region DBSets

    public DbSet<Hero> TBHeroes => Set<Hero>();

    public DbSet<Superpower> TBSuperpowers => Set<Superpower>();

    public DbSet<HeroSuperpower> TBHeroesSuperpowers => Set<HeroSuperpower>();

    #endregion

    #region ModelBuilder e SaveChanges

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                 .SelectMany(e => e.GetProperties()
                     .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(256)");

        ApplyConfigurationsFromEntity(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.EnableSensitiveDataLogging(false);

    /// <summary>
    /// Configuração do moelBuilder.Entity das tabelas
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected void ApplyConfigurationsFromEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hero>().ToTable("tb_Heroes").HasKey(t => t.Code);
        modelBuilder.Entity<Superpower>().ToTable("tb_Superpowers").HasKey(t => t.Code);
        modelBuilder.Entity<HeroSuperpower>().ToTable("tb_HeroesSuperpowers").HasKey(x => new { x.CodeHero, x.CodeSuperpower });       
    }

    #endregion
}