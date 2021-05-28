using DominandoEF.Api.Data.Repository;

namespace DominandoEF.Api.Data
{
    public interface IUnitOfWork
    {
        bool Commit();
        IDepartamentoRepository DepartamentoRepository { get; }
    }
}
