using Heroes.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heroes.Core.Data.EntityConfig;

public class SuperpowerConfiguration : IEntityTypeConfiguration<Superpower>
{
    public void Configure(EntityTypeBuilder<Superpower> builder)
    {
        builder.ToTable("tb_Superpowers", "dbo");

        builder
            .Property(p => p.Code)
            .HasColumnName("Id");

        builder
            .HasKey(p => p.Code)
            .HasName("PK_tb_Superpowers");

        builder
            .Property(p => p.Description)
            .HasColumnType("varchar(250)")
            .IsRequired()
            .HasColumnName("Description");

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

        builder
            .Ignore(s => s.Name);
       
    }
}