using DominandoEF.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace DominandoEF.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
    }
}
