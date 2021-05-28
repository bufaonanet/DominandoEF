using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEF.Data.Configurations
{
    class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.Property("_cpf").HasColumnName("CPF").HasMaxLength(11);
            //.HasField("_cpf");
        }
    }
}
