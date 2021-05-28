using DominandoEF.Tests.Domain;
using Microsoft.EntityFrameworkCore;

namespace DominandoEF.Tests.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

    }
}
