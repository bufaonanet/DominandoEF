using DominandoEF.Data;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominandoEF.Modulos
{
    public static class Relacionamentos
    {
        public static void Relacionamento1x1()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var estado = new Estado
            {
                Nome = "MG",
                Governador = new Governador { Nome = "Douglas", Partido = "Sem partido" }
            };

            db.Estados.Add(estado);
            db.SaveChanges();

            var estados = db.Estados.AsNoTracking().ToList();

            estados.ForEach(e =>
            {
                Console.WriteLine($"Estado:{e.Nome} - Governador:{e.Governador.Nome}");
            });
        }

        public static void Relacionamento1xN()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var estado = new Estado
                {
                    Nome = "MG",
                    Governador = new Governador { Nome = "Douglas", Partido = "Sem partido" }
                };

                estado.Cidades.Add(new Cidade { Nome = "Guanhães" });

                db.Estados.Add(estado);
                db.SaveChanges();
            }

            using (var db = new ApplicationContext())
            {
                var estados = db.Estados.ToList();
                estados[0].Cidades.Add(new Cidade { Nome = "Belo Horizonte" });

                db.SaveChanges();

                foreach (var estado in db.Estados.Include(p => p.Cidades).AsNoTracking())
                {
                    Console.WriteLine($"Estado: {estado.Nome} - Governador: {estado.Governador.Nome}");

                    foreach (var cidade in estado.Cidades)
                    {
                        Console.WriteLine($"\t Cidade: {cidade.Nome}");
                    }
                }
            }
        }

        public static void RelacionamentoNxN()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var ator1 = new Ator { Nome = "Rafael" };
                var ator2 = new Ator { Nome = "Eduardo" };
                var ator3 = new Ator { Nome = "Bruno" };

                var filme1 = new Filme { Descricao = "A volta dos que não foram" };
                var filme2 = new Filme { Descricao = "De volta para o futuro" };
                var filme3 = new Filme { Descricao = "Poeira em alto mar" };

                ator1.Filmes.Add(filme1);
                ator1.Filmes.Add(filme2);

                ator2.Filmes.Add(filme1);

                filme3.Atores.Add(ator1);
                filme3.Atores.Add(ator2);
                filme3.Atores.Add(ator3);

                db.AddRange(ator1, ator2, filme3);
                db.SaveChanges();

                foreach (var ator in db.Atores.Include(p => p.Filmes))
                {
                    Console.WriteLine($"Ator: {ator.Nome}");

                    foreach (var filme in ator.Filmes)
                    {
                        Console.WriteLine($"\t Filmes: {filme.Descricao}");
                    }
                }
            }
        }

        public static void CampoDeApoio()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var documento = new Documento();
                documento.SetCpf("33669988");

                db.Documentos.Add(documento);
                db.SaveChanges();

                foreach (var doc in db.Documentos.AsNoTracking())
                {
                    Console.WriteLine($"Documento: {doc.GetCpf()}");
                }
            }
        }

        public static void ExemploTabelasPorHeranca()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var pessoa = new Pessoa { Nome = "Pessoa Fulano de tal" };
                var instrutor = new Instrutor { Nome = "Instrutor Bufão", Tecnologia = ".net", Deste = DateTime.Now };
                var aluno = new Aluno { Nome = "Aluno Douglas", Idade = 34, DataContrato = DateTime.Now.AddDays(-5) };

                db.AddRange(pessoa, instrutor, aluno);
                db.SaveChanges();

                var pessoas = db.Pessoas.AsNoTracking().ToArray();
                var instrutores = db.Instrutors.AsNoTracking().ToArray();
                //var alunos = db.Alunos.AsNoTracking().ToArray();
                var alunos = db.Alunos.OfType<Aluno>().AsNoTracking().ToArray();

                Console.WriteLine("Pessoas *****************************");
                foreach (var p in pessoas)
                {
                    Console.WriteLine($"Pessoa: {p.Id} -> {p.Nome}");
                }

                Console.WriteLine("Instrutores *****************************");
                foreach (var p in instrutores)
                {
                    Console.WriteLine($"Pessoa: {p.Id} -> {p.Nome}, Tecnologias {p.Tecnologia}, Desde {p.Deste}");
                }

                Console.WriteLine("Alunos *****************************");
                foreach (var p in alunos)
                {
                    Console.WriteLine($"Pessoa: {p.Id} -> {p.Nome}, Idade {p.Idade}, Contrato {p.DataContrato}");
                }
            }
        }

        public static void PacoteDePropriedade()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var configuracoe = new Dictionary<string, object>
                {
                    ["Chave"] = "SenhaDoBanco",
                    ["Valor"] = Guid.NewGuid().ToString()
                };

                db.Configuracoes.Add(configuracoe);
                db.SaveChanges();

                var configuracoes = db.Configuracoes
                    .AsNoTracking()
                    .Where(p => p["Chave"] == "SenhaDoBanco")
                    .ToArray();

                foreach (var config in configuracoes)
                {
                    Console.WriteLine($"Chave: {config["Chave"]} - Valor:{config["Valor"]}");
                }
            }
        }

        public static void TiposDePropriedades()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var cliente = new Cliente
            {
                Nome = "Fulano",
                Telefone = "3398831064",
                Endereco = new Endereco { Bairro = "Centro", Cidade = "Guanhães" }
            };

            db.Clientes.Add(cliente);
            db.SaveChanges();

            var clientes = db.Clientes.AsNoTracking().ToList();

            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };

            clientes.ForEach(cli =>
            {
                var json = System.Text.Json.JsonSerializer.Serialize(cli, options);
                Console.WriteLine(json);
            });
        }

        public static void TrabalhandoComPropriedadesSombra()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var departamento = new Departamento { Descricao = "Departamento usando shadow property" };

            db.Departamentos.Add(departamento);
            db.Entry(departamento).Property("UltimaAtualizacao").CurrentValue = DateTime.Now;
            db.SaveChanges();

            var consultaDepartamento = db.Departamentos
                .Where(p => EF.Property<DateTime>(p, "UltimaAtualizacao") < DateTime.Now)
                .ToArray();

        }

        public static void ConversorCustomizado()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Conversores.Add(new Conversor { Status = Status.Devolvido });
            db.SaveChanges();

            var conversorEmAnalise = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Status.Analise);
            var conversorDevolvido = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Status.Devolvido);

        }
    }
}
