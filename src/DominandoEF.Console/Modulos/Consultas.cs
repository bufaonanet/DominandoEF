using DominandoEF.Data;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Modulos
{
    public static class Consultas
    {
        public static void ConsultaDividida()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var departamentos = db.Departamentos
               .Include(p => p.Funcionarios)
               //.AsSplitQuery()
               //.AsSingleQuery()
               .Where(p => p.Id < 3)
               .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");

                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\t Nome: {funcionario.Nome}");
                }
            }
        }

        public static void Consultas1NxN1()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var funcionarios = db.Funcionarios
               .Include(p => p.Departamento)
               .ToList();

            foreach (var funcionario in funcionarios)
            {
                Console.WriteLine($"Nome: {funcionario.Nome} / Departamento: {funcionario.Departamento.Descricao}");
            }


            //var departamentos = db
            //    .Departamentos
            //    .Include(p => p.Funcionarios)
            //    .ToList();

            //foreach (var departamento in departamentos)
            //{
            //    Console.WriteLine($"Descrição: {departamento.Descricao}");

            //    foreach (var funcionario in departamento.Funcionarios)
            //    {
            //        Console.WriteLine($"\t Nome: {funcionario.Nome}");
            //    }
            //}
        }

        public static void ConsultaComTag()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var departamentos = db.Departamentos
                .TagWith(@"
                        Primeiro comentário
                        Segundo comentário")
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");
            }
        }

        public static void ConsultaInterpolada()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            int id = 1;

            var departamentos = db.Departamentos
                .FromSqlInterpolated($"SELECT * FROM departamentos WHERE id > {id}")
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");
            }
        }

        public static void ConsultaParametricada()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            //var id = new SqlParameter { Value = 1, SqlDbType = System.Data.SqlDbType.Int };
            int id = 0;

            var departamentos = db.Departamentos
                .FromSqlRaw("SELECT * FROM departamentos WHERE id > {0}", id)
                .Where(p => !p.Excluido)
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");
            }
        }

        public static void ConsultaProjetada()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var departamentos = db.Departamentos
                .Where(p => p.Id > 0)
                .Select(p => new { p.Descricao, Funcionarios = p.Funcionarios.Select(f => f.Nome) })
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");

                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\t Nome: {funcionario}");
                }
            }

        }

        public static void IgnorarFiltroGlobal()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var departamentos = db.Departamentos.IgnoreQueryFilters().Where(p => p.Id > 0).ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao} \t Excluído: {departamento.Excluido} ");
            }

        }

        public static void FiltroGlobal()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var departamentos = db.Departamentos.Where(p => p.Id > 0).ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao} \t Excluído: {departamento.Excluido} ");
            }

        }
        private static void SetUp(ApplicationContext db)
        {
            if (!db.Departamentos.Any())
            {
                db.Departamentos.AddRange(
                    new Departamento
                    {
                        Descricao = "Departamento 01",
                        Ativo = true,
                        Excluido = false,
                        Funcionarios = new List<Funcionario>
                        {
                            new Funcionario{Nome = "Douglas", CPF="1111111", RG="aaaaaaa"}
                        }
                    },
                    new Departamento
                    {
                        Descricao = "Departamento 02",
                        Ativo = true,
                        Excluido = false,
                        Funcionarios = new List<Funcionario>
                        {
                            new Funcionario{Nome = "Bufão", CPF="222222", RG="ccccccc"},
                            new Funcionario{Nome = "Zezinho", CPF="333333", RG="dddddddd"},
                        }
                    },
                    new Departamento
                    {
                        Descricao = "Departamento Excluido",
                        Ativo = true,
                        Excluido = true
                    }
                );

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
        }

        public static void ConsultandoDepartametos()
        {
            using var db = new ApplicationContext();

            var departamentos = db.Departamentos.Where(p => p.Id > 0).ToArray();
        }
    }
}
