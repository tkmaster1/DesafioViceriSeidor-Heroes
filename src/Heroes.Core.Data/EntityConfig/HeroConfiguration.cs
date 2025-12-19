using Heroes.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heroes.Core.Data.EntityConfig;

public class HeroConfiguration : IEntityTypeConfiguration<Hero>
{
    public void Configure(EntityTypeBuilder<Hero> builder)
    {
        builder.ToTable("tb_Heroes", "dbo");

        builder
            .Property(p => p.Code)
            .HasColumnName("Id");

        builder
            .HasKey(p => p.Code)
            .HasName("PK_tb_Heroes");

        builder
            .Property(p => p.Name)
            .HasColumnType("varchar(120)")
            .IsRequired()
            .HasColumnName("Name");

        builder
            .Property(p => p.HeroName)
            .HasColumnType("varchar(120)")
            .IsRequired()
            .HasColumnName("HeroName");

        builder
            .HasIndex(h => h.HeroName)
            .IsUnique()
            .HasDatabaseName("UQ_tb_Heroes_HeroName");

        builder
            .Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnName("BirthDate");

        builder
            .Property(p => p.Height)
            .IsRequired()
            .HasColumnName("Height");

        builder
            .Property(p => p.Weight)
            .IsRequired()
            .HasColumnName("Weight");

        builder
            .Property(p => p.DateCreate)
            .IsRequired()
            .HasColumnName("DateCreate");

        builder
            .Property(p => p.DateChange)
            .HasColumnName("DateChange");

        builder
            .Property(p => p.Status)
            .IsRequired()
            .HasColumnName("Status");

    }
}