using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Data.Configurations
{
    class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //Mapeando ValueObjects
            builder.OwnsOne(x => x.Endereco, end =>
            {
                end.Property(p => p.Bairro).HasColumnName("Bairro");
                end.ToTable("Enderecos");
            });           

        }
    }
}
