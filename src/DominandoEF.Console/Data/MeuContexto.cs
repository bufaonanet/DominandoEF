using DominandoEF.Domain;
using DominandoEF.Interceptadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DominandoEF.Data
{
    public class MeuContexto : DbContext
    {
        public DbSet<Funcao> Funcoes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //const string connection = @"Server=BUFAONANET;Database=CursoEF; User Id=SA; Password=admin123@; Trusted_Connection=True;";
            const string connection = @"Server=(localdb)\mssqllocaldb;Initial Catalog=DominandoEF; Integrated Security=true; pooling = true;";

            optionsBuilder
                .UseSqlServer(connection)
                .LogTo(Console.WriteLine, LogLevel.Information)//habilita os logs do EF Core no console
                .EnableSensitiveDataLogging() //Mostra o valor dos parâmetros passados no log
                .AddInterceptors(new InterceptadorDeComandos())
                //.UseSqlite("Data source=CursoEF.db") //Usando sqlite
                //.UseInMemoryDatabase(databaseName:"db-teste")
                ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Funcao>(conf=>
                {
                    conf.Property<string>("PropriedadeSombra")
                        .HasColumnType("varchar(100)")
                        .HasDefaultValueSql("'Teste'");
                });
        }
    }
}
