using DominandoEF.Conversores;
using DominandoEF.Data.Configurations;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DominandoEF.Data
{
    public class ApplicationContext : DbContext
    {

        //private readonly StreamWriter _writer = new StreamWriter("meu_log.txt", append: true);
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Conversor> Conversores { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Ator> Atores { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Instrutor> Instrutors { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Livro> Livros { get; set; }            
        public DbSet<Atributo> Atributos { get; set; }
        public DbSet<Aeroporto> Aeroportos { get; set; }
        public DbSet<RelatorioFinanceiro> RelatorioFinanceiros { get; set; }
        public DbSet<Dictionary<string, object>> Configuracoes => Set<Dictionary<string, object>>("Configuracoes");


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = @"Server=(localdb)\mssqllocaldb;Initial Catalog=DominandoEF; Integrated Security=true; pooling = true;";
            //const string connectionString = @"Server=BUFAONANET;Database=DominandoEF; User Id=SA; Password=admin123@; Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging() //Mostra o valor dos parâmetros passados no log
                .LogTo(Console.WriteLine, LogLevel.Information)//habilita os logs do EF Core no console
            //.EnableDetailedErrors() //Exibindo erros detalhados
            /*.UseSqlServer(connectionString, 
                o=>o.MaxBatchSize(100) //usando batchsize
                    .CommandTimeout(5) //Definindo timeout para comandos
                    .EnableRetryOnFailure(4,TimeSpan.FromSeconds(10),null) //usando resiliênica para conexão
             )*/
            //.UseSqlServer(connectionString, p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)) //usar spliquery global
            //.UseLazyLoadingProxies() //Proxies para usar Lazy load
            /*.LogTo(Console.WriteLine, 
                   new[] {CoreEventId.ContextInitialized, RelationalEventId.CommandExecuted },
                   LogLevel.Information,
                   DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine);*/
            //.LogTo(_writer.WriteLine, LogLevel.Information) //Salvando log em arquivo de texto
            ;



            //optionsBuilder.UseSqlServer(connection);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Departamento>().HasQueryFilter(p => !p.Excluido); //Adicioando Filtro global

            //modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI"); //Adicionando Collations de forma global

            //Collations para propriedade
            //modelBuilder.Entity<Departamento>().Property(p => p.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS");

            //modelBuilder
            //    .HasSequence<int>("MinhaSequencia", "sequencias")
            //    .StartsAt(1)
            //    .IncrementsBy(2)
            //    .HasMin(1)
            //    .HasMax(10)
            //    .IsCyclic();

            //modelBuilder.Entity<Departamento>().Property(p => p.Id)
            //    .HasDefaultValueSql("NEXT VALUE FOR sequencias.MinhaSequencia");

            //modelBuilder
            //    .Entity<Departamento>()
            //    .HasIndex(p => new { p.Descricao, p.Ativo })
            //    .HasDatabaseName("idx_meu_indice")
            //    .HasFilter("Descricao IS NOT NULL")
            //    .HasFillFactor(80)
            //    .IsUnique();

            //Semeando dados
            //modelBuilder.Entity<Estado>().HasData(new[] 
            //{
            //    new Estado{ Id = 1,Nome="MG"},
            //    new Estado{ Id = 2,Nome="SP"},
            //});

            //modelBuilder.HasDefaultSchema("cadastros");
            //modelBuilder.Entity<Estado>().ToTable("Estado","segundo_esquema");

            //var conversao = new EnumToStringConverter<Versao>();

            //modelBuilder.Entity<Conversor>()
            //    .Property(p => p.Versao)
            //    .HasConversion<string>()
            //    .HasConversion(conversao);

            //modelBuilder.Entity<Conversor>()
            //    .Property(p => p.Status)
            //    .HasConversion(new ConversorCustomizado());

            //shadow property
            //modelBuilder.Entity<Departamento>().Property<DateTime>("UltimaAtualizacao");

            //Maneiras de importar configurações de mapeamento de classes
            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            //modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Configuracoes", b => 
            //{
            //    b.Property<int>("Id");
            //    b.Property<string>("Chave").HasColumnType("VARCHAR(40)").IsRequired();
            //    b.Property<string>("Valor").HasColumnType("VARCHAR(255)").IsRequired();
            //});


        }

        public override void Dispose()
        {
            base.Dispose();
           // _writer.Dispose();
        }
    }
}
