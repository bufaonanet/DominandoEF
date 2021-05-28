using DominandoEF.Migracoes.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Migracoes.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connections = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=EF_Migracoes; Integrated Security=true;";

            optionsBuilder
                .UseSqlServer(connections)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(conf =>
            {
                conf.HasKey(p => p.Id);
                conf.Property(p => p.Nome).HasMaxLength(255).IsUnicode(false);
            });
        }
    }
}
