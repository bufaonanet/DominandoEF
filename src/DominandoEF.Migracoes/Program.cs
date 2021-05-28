using System;
using Microsoft.EntityFrameworkCore;

namespace DominandoEF.Migracoes
{
    class Program
    {
        static void Main()
        {
            using var db = new Data.ApplicationContext();

            //db.Database.Migrate();

            var migracoesPendentes = db.Database.GetPendingMigrations();
            foreach (var migracao in migracoesPendentes)
            {
                System.Console.WriteLine(migracao);
            };
        }
    }
}
