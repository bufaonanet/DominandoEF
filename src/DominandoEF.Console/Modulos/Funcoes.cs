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
    public static class Funcoes
    {
        private static void SetParaFuncoes()
        {
            using var db = new MeuContexto();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Funcoes.AddRange(
            new Funcao
            {
                Data1 = DateTime.Now.AddDays(2),
                Data2 = "2021-05-26",
                Descricao1 = "Bala 1",
                Descricao2 = "Bala 2"
            },
            new Funcao
            {
                Data1 = DateTime.Now.AddDays(1),
                Data2 = "xx21-05-26",
                Descricao1 = "Bola 1",
                Descricao2 = "Bola 2 "
            },
            new Funcao
            {
                Data1 = DateTime.Now,
                Data2 = "xx21-05-26",
                Descricao1 = "Tela",
                Descricao2 = "Tela"
            });

            db.SaveChanges();
        }

        public static void FuncaoDeDatas()
        {
            SetParaFuncoes();

            using (var db = new MeuContexto())
            {
                var script = db.Database.GenerateCreateScript();

                Console.WriteLine(script);

                var dados = db.Funcoes.AsNoTracking().Select(p => new
                {
                    Dias = EF.Functions.DateDiffDay(DateTime.Now, p.Data1),
                    Data = EF.Functions.DateFromParts(2021, 5, 26),
                    DataValida = EF.Functions.IsDate(p.Data2)
                });

                foreach (var f in dados)
                {
                    Console.WriteLine(f);
                }
            }
        }

        public static void FuncaoLike()
        {
            using (var db = new MeuContexto())
            {
                var dados = db
                    .Funcoes
                    .AsNoTracking()
                    //.Where(p => EF.Functions.Like(p.Descricao1, "%Bo%"))
                    .Where(p => EF.Functions.Like(p.Descricao1, "B[oa]%"))

                    .Select(p => p.Descricao1);

                Console.WriteLine("Resultado da projeção");
                foreach (var descricao in dados)
                {
                    Console.WriteLine(descricao);
                }
            }
        }

        public static void FuncaoDataLenght()
        {
            using var db = new MeuContexto();

            var resultado = db
                .Funcoes
                .AsNoTracking()
                .Select(p => new
                {
                    TotalBytesCampoData = EF.Functions.DataLength(p.Data1),
                    TotalBytes1 = EF.Functions.DataLength(p.Descricao1),
                    TotalBytes2 = EF.Functions.DataLength(p.Descricao2),
                    Total1 = p.Descricao1.Length,
                    Total2 = p.Descricao2.Length
                })
                .FirstOrDefault();

            Console.WriteLine("Resultado da projeção");
            Console.WriteLine(resultado);
        }

        public static void FuncaoProperty()
        {
            SetParaFuncoes();

            using var db = new MeuContexto();

            var resultado = db
                .Funcoes
                //.AsNoTracking()
                .FirstOrDefault(p => EF.Property<string>(p, "PropriedadeSombra") == "Teste");

            var propriedadeSombra = db
                .Entry(resultado)
                .Property<string>("PropriedadeSombra")
                .CurrentValue;

            Console.WriteLine("Resultado da projeção");
            Console.WriteLine(propriedadeSombra);
        }

        public static void FuncaoCollate()
        {
            using var db = new MeuContexto();

            var consulta1 = db
                .Funcoes
                .FirstOrDefault(p => EF.Functions.Collate(p.Descricao1, "SQL_Latin1_General_CP1_CI_AS") == "tela");

            var consulta2 = db
                .Funcoes
                .FirstOrDefault(p => EF.Functions.Collate(p.Descricao1, "SQL_Latin1_General_CP1_CS_AS") == "Tela");

            Console.WriteLine($"Consulta 1: {consulta1.Descricao1}");
            Console.WriteLine($"Consulta 2: {consulta2?.Descricao1}");

        }

        public static void TesteIntercepcao()
        {
            using var db = new MeuContexto();

            var consulta = db
                .Funcoes
                .FirstOrDefault();            

            Console.WriteLine($"Consulta 1: {consulta.Descricao1}");
        }



    }
}
