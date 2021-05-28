using DominandoEF.Data;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;

namespace DominandoEF.Modulos
{
    public static class InfraEstrutura
    {
        private static void ExecutarEstrategiaResiliencia()
        {
            using var db = new ApplicationContext();
            var strategy = db.Database.CreateExecutionStrategy();

            strategy.Execute(() =>
            {
                using var transaction = db.Database.BeginTransaction();

                db.Departamentos.Add(new Departamento { Descricao = "Departamento teste" });
                db.SaveChanges();

                transaction.Commit();
            });
        }

        public static void TempoComandoGeral()
        {
            using var db = new ApplicationContext();

            db.Database.SetCommandTimeout(10);
            db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:07'; select 1");
        }

        public static void HabilitarBatchSize()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            for (int i = 0; i < 50; i++)
            {
                db.Departamentos.AddRange(new Departamento { Descricao = "Departamento " + i });
            }

            db.SaveChanges();
        }
    }
}
