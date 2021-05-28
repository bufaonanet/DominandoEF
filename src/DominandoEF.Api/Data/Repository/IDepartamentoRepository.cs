using DominandoEF.Api.Data.Repository.Base;
using DominandoEF.Api.Domain;
using System.Threading.Tasks;

namespace DominandoEF.Api.Data.Repository
{
    public interface IDepartamentoRepository : IGenericRepository<Departamento>
    {
        //Task<Departamento> GetByIdAsync(int id);
        //void Add(Departamento departamento);
        //bool Save();
    }
}
