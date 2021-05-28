using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DominandoEF.Customizacoes
{
    class Program
    {
        static void Main()
        {
            using var db = new Data.MeuDBContext();

            var sql = db.Departamentos.Where(p => p.Id > 0).ToQueryString();

            Console.WriteLine(sql);
        }
    }
}
