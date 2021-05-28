using DominandoEF.Data;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DominandoEF.Modulos
{
    public static class PerformanceConsultas
    {
        public static void ConsultaRastreada()
        {
            //using var db = new MeuContexto();
            using var db = new ApplicationContext();
            var funcionarios = db.Funcionarios.Include(p => p.Departamento).ToList();
        }

        public static void ConsultaNaoRastreada()
        {
            //using var db = new MeuContexto();
            using var db = new ApplicationContext();
            var funcionarios = db.Funcionarios.AsNoTracking().Include(p => p.Departamento).ToList();
        }

        public static void ConsultaComResolucaoDeIdentidade()
        {
            //using var db = new MeuContexto();
            using var db = new ApplicationContext();
            var funcionarios = db.Funcionarios
                .AsNoTrackingWithIdentityResolution()
                .Include(p => p.Departamento)
                .ToList();
        }

        public static void ConsultaProjetadaRastreada()
        {
            //using var db = new MeuContexto();
            using var db = new ApplicationContext();
            var departamentos = db.Departamentos
                .Include(p => p.Funcionarios)
                .Select(p => new
                {
                    Departamento = p,
                    TotalFuncionarios = p.Funcionarios.Count
                }).ToList();

            departamentos[0].Departamento.Descricao = "Departamento atualizado";

            db.SaveChanges();
        }

        public static void Inserir_200_departamentos_com_1mb()
        {
            //using var db = new MeuContexto();
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var random = new Random();

            db.Departamentos.AddRange(Enumerable.Range(1, 200).Select(p =>
                new Departamento
                {
                    Descricao = "Departamento de teste",
                    Imagem = getBytes()
                }));

            db.SaveChanges();

            byte[] getBytes()
            {
                var buffer = new byte[1024 * 1024];
                random.NextBytes(buffer);
                return buffer;
            }
        }

        public static void ConsultaProjetada()
        {
            //using var db = new MeuContexto();
            using var db = new ApplicationContext();

            //var departamentos = db.Departamentos.ToArray();
            var departamentos = db.Departamentos.Select(p => p.Descricao).ToArray();
           
            var memoria = (System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024) + " MB";

            Console.WriteLine(memoria);
        }

    }
}
