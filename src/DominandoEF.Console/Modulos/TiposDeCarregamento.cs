using DominandoEF.Data;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominandoEF.Modulos
{
    public static class TiposDeCarregamento
    {
        public static void CarregamentoPreguicoso()
        {
            using var db = new ApplicationContext();

            //db.ChangeTracker.LazyLoadingEnabled = false;

            var departamentos = db.Departamentos.ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine($"Departamento: {departamento.Descricao}");


                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        Console.WriteLine($"Funcionário: {funcionario.Nome}");
                    }
                }
                else
                {
                    Console.WriteLine($"Departamento sem funcionários");
                }
            }
        }

        public static void CarregamentoExplicito()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var departamentos = db.Departamentos.ToList();

            foreach (var departamento in departamentos)
            {
                if (departamento.Id == 2)
                {
                    //db.Entry(departamento).Collection(p => p.Funcionarios).Load();
                    db.Entry(departamento).Collection(p => p.Funcionarios)
                        .Query().Where(p => p.Id > 2).ToList();
                }

                Console.WriteLine("------------------------------------");
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        Console.WriteLine($"Funcionário: {funcionario.Nome}");
                    }
                }
                else
                {
                    Console.WriteLine($"Departamento sem funcionários");
                }
            }
        }
        public static void CarregamentoAdiantado()
        {
            using var db = new ApplicationContext();
            SetUp(db);

            var departamentos = db
                .Departamentos
                .Include(d => d.Funcionarios);

            foreach (var departamento in departamentos)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        Console.WriteLine($"Funcionário: {funcionario.Nome}");
                    }
                }
                else
                {
                    Console.WriteLine($"Departamento sem funcionários");
                }
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
    }
}
