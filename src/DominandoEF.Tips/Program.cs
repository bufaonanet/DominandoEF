using DominandoEF.Tips.Data;
using DominandoEF.Tips.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DominandoEF.Tips
{
    class Program
    {
        static void Main()
        {
            //ToQueryString();
            //DebugView();
            //Clear();
            //ConsultaFiltrada();
            //SemChavePrimaria();
            //ToView();
            //NaoUnicode();
            //OperadoresAgregacao();
            //OperadoresAgregacaoNoAgrupamento();
            ContadorDeEventos();
        }

        private static void ContadorDeEventos()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine($" PID: {System.Diagnostics.Process.GetCurrentProcess().Id}");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                var departamento = new Departamento { Descricao = "Departamento sem colaborador" };

                db.Departamentos.Add(departamento);
                db.SaveChanges();

                _ = db.Departamentos.Find(1);
                _ = db.Departamentos.AsNoTracking().FirstOrDefault();
            }
        }

        private static void OperadoresAgregacaoNoAgrupamento()
        {
            using var db = new ApplicationContext();

            var sql = db.Departamentos
                .GroupBy(p => p.Descricao)
                .Where(p => p.Count() == 2)
                .Select(p => new
                {
                    Descricao = p.Key,
                    Contador = p.Count()
                }).ToQueryString();

            Console.WriteLine(sql);
        }

        private static void OperadoresAgregacao()
        {
            using var db = new ApplicationContext();

            var sql = db.Departamentos
                .GroupBy(p => p.Descricao)
                .Select(p => new
                {
                    Descricao = p.Key,
                    Contador = p.Count(),
                    Media = p.Average(p => p.Id),
                    Maximo = p.Max(p => p.Id),
                    Soma = p.Sum(p => p.Id)
                }).ToQueryString();

            Console.WriteLine(sql);
        }

        private static void NaoUnicode()
        {
            using var db = new ApplicationContext();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        private static void ToView()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Database.ExecuteSqlRaw(
            @"CREATE VIEW vw_departamento_relatorio AS
              SELECT 
                d.Descricao, count(c.Id) as Colaboradores
              FROM Departamentos d
              LEFT JOIN Colaboradores c on c.DepartamentoId = d.Id
              GROUP BY d.Descricao                
            ");

            var departamentos = Enumerable.Range(1, 10).Select(p => new Departamento
            {
                Descricao = $"Departamento {p}",
                Colaboradores = Enumerable.Range(1, p).Select(c => new Colaborador
                {
                    Nome = $"Colaborador {p}-{c}"
                }).ToList()
            });

            var departamento = new Departamento { Descricao = "Departamento sem colaborador" };

            db.Departamentos.Add(departamento);
            db.Departamentos.AddRange(departamentos);
            db.SaveChanges();

            var relatorio = db.DepartamentoRelatorio
                .Where(p => p.Colaboradores < 20)
                .OrderBy(p => p.Departamento)
                .ToList();

            foreach (var dep in relatorio)
            {
                Console.WriteLine($"{dep.Departamento} [Colaboradores: {dep.Colaboradores}]");
            }

        }

        private static void SemChavePrimaria()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var usuarioFuncaos = db.UsuarioFuncoes.Where(p => p.UsuarioId == Guid.NewGuid()).ToArray();
        }

        private static void SingleOrDefaultVsFirstOrDefault()
        {
            using var db = new ApplicationContext();

            var query = db
                .Departamentos
                .Include(p => p.Colaboradores.Where(p => p.Nome.Contains("novo")))
                .Where(p => p.Descricao.ToUpper().Contains("TESTE"))
                .ToQueryString();

            Console.WriteLine(query);
        }

        private static void ConsultaFiltrada()
        {
            using var db = new ApplicationContext();

            var query = db
                .Departamentos
                .Include(p => p.Colaboradores.Where(p => p.Nome.Contains("novo")))
                .Where(p => p.Descricao.ToUpper().Contains("TESTE"))
                .ToQueryString();

            Console.WriteLine(query);
        }

        private static void Clear()
        {
            using var db = new ApplicationContext();

            db.Departamentos.Add(new Departamento { Descricao = "Teste Debugview" });

            db.ChangeTracker.Clear();
        }

        private static void DebugView()
        {
            using var db = new ApplicationContext();

            db.Departamentos.Add(new Departamento { Descricao = "Teste Debugview" });

            var query = db.Departamentos.Where(p => p.Id > 1);
        }

        private static void ToQueryString()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            var query = db.Departamentos.Where(p => p.Id > 1);

            var sql = query.ToQueryString();

            Console.WriteLine(sql);
        }
    }
}
