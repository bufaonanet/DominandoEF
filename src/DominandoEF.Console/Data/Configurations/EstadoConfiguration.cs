using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEF.Data.Configurations
{
    class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder
                 .HasOne(p => p.Governador)
                 .WithOne(p => p.Estado)
                 .HasForeignKey<Governador>(p => p.EstadoId);

            builder.Navigation(p => p.Governador).AutoInclude();

            builder
                .HasMany(p => p.Cidades)
                .WithOne(p => p.Estado)
                .IsRequired(false);

        }
    }
}
