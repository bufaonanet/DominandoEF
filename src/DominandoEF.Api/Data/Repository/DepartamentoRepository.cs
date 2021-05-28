using DominandoEF.Api.Data.Repository.Base;
using DominandoEF.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DominandoEF.Api.Data.Repository
{
    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
    {
        //private readonly ApplicationContext _context;
        //private readonly DbSet<Departamento> _dbSet;

        public DepartamentoRepository(ApplicationContext context) : base(context)
        {
            //_context = context;
            //_dbSet = context.Set<Departamento>(); //_dbSet = context.Departamentos;
        }

        //public void Add(Departamento departamento)
        //{
        //    _dbSet.Add(departamento);
        //}

        //public async Task<Departamento> GetByIdAsync(int id)
        //{
        //    return await _dbSet
        //        .Include(p => p.Colaboradores)
        //        .FirstOrDefaultAsync(p => p.Id == id);
        //}

        //public bool Save()
        //{
        //    return _context.SaveChanges() > 0;
        //}
    }
}
