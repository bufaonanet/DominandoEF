using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominandoEF.Data.Configurations
{
    class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");
        }
    }

    class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
    {
        public void Configure(EntityTypeBuilder<Instrutor> builder)
        {
            builder.ToTable("Instrutores");
        }
    }

    class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Aluno");
        }
    }

}
