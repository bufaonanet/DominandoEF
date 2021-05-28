using DominandoEF.Customizacoes.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Customizacoes.Data
{
    public class MeuDBContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connection = "Server=(localdb)\\msqllocaldb;Database=sobrescrevendo_comportamentos;Integrated Security=true;";

            optionsBuilder
                .UseSqlServer(connection)
                .ReplaceService<IQuerySqlGeneratorFactory, MySqlServerQuerySqlGeneratorFactory>()
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                ;
        }
    }
}
