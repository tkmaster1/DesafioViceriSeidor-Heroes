using Heroes.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heroes.Core.Data.EntityConfig;

public class HeroSuperpowerConfiguration : IEntityTypeConfiguration<HeroSuperpower>
{
    public void Configure(EntityTypeBuilder<HeroSuperpower> builder)
    {
        builder.ToTable("tb_HeroesSuperpowers", "dbo");

        builder
            .Property(p => p.CodeHero)
            .HasColumnName("HeroId");

        builder
            .Property(p => p.CodeSuperpower)
            .HasColumnName("SuperpowerId");

        builder
            .HasKey(hs => new { hs.CodeHero, hs.CodeSuperpower })
            .HasName("[PK_tb_HeroesSuperpowers");

        builder
            .Ignore(s => s.DateCreate);

        builder
            .Ignore(s => s.DateChange);

        builder
            .Ignore(s => s.Status);

        builder
            .Ignore(s => s.Name);

        builder
            .Ignore(s => s.Code);

        builder
            .HasOne(hs => hs.Hero)
            .WithMany(h => h.HeroSuperpowers)
            .HasForeignKey(hs => hs.CodeHero)
            .HasConstraintName("FK_tb_HeroesSuperpowers_tb_Heroes_HeroId");
          //  .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(hs => hs.Superpower)
            .WithMany(sp => sp.HeroSuperpowers)
            .HasForeignKey(hs => hs.CodeSuperpower)
            .HasConstraintName("FK_tb_HeroesSuperpowers_tb_Superpowers_SuperpowerId");
    }
}