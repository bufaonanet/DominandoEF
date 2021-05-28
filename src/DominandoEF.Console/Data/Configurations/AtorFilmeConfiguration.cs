using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace DominandoEF.Data.Configurations
{
    class AtorFilmeConfiguration : IEntityTypeConfiguration<Ator>
    {
        public void Configure(EntityTypeBuilder<Ator> builder)
        {
            //builder
            //    .HasMany(p => p.Filmes)
            //    .WithMany(p => p.Atores)
            //    .UsingEntity(p => p.ToTable("Atores_Filmes"));

            builder
                .HasMany(p => p.Filmes)
                .WithMany(p => p.Atores)
                .UsingEntity<Dictionary<string, object>>(
                    "Atores_Filmes",
                    p => p.HasOne<Filme>().WithMany().HasForeignKey("FilmeId"),
                    p => p.HasOne<Ator>().WithMany().HasForeignKey("AtorId"),
                    p =>
                    {
                        p.Property<DateTime>("CadastradoEm").HasDefaultValueSql("GETDATE()");
                    }                    
                );
        }
    }
}
