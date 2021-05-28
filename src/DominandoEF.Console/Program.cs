using DominandoEF.Data;
using DominandoEF.Domain;
using DominandoEF.Modulos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominandoEF
{
    class Program
    {
        //static int _count;

        static void Main()
        {
            //ManipularDB.EnsureCreated();
            //ManipularDB.CriandoDBMultiploesContextos();
            //ManipularDB.HealthCheckBancoDeDados();
            //_count = 0;
            //ManipularDB.GerenciandoEstadoDaConexao(true, _count);
            //_count = 0;
            //ManipularDB.GerenciandoEstadoDaConexao(false, _count);
            //ManipularDB.ExecuteSql();
            //ManipularDB.SqlInjection();
            //ManipularDB.MigracoesPendentes();
            //ManipularDB.AplicarMigracoesPendentes();
            //ManipularDB.ObterTodasMigracoes();
            //ManipularDB.MigracoesJaExecutadas();
            //ManipularDB.RecriarBanco();
            //ManipularDB.ScriptGeradoBancoDeDados();

            //TiposDeCarregamento.CarregamentoAdiantado();
            //TiposDeCarregamento.CarregamentoExplicito();
            //TiposDeCarregamento.CarregamentoPreguicoso();

            //Consultas.FiltroGlobal();
            //Consultas.IgnorarFiltroGlobal();
            //Consultas.ConsultaProjetada();
            //Consultas.ConsultaParametricada();
            //Consultas.ConsultaInterpolada();
            //Consultas.ConsultaComTag();
            //Consultas.Consultas1NxN1();
            //Consultas.ConsultaDividida();
            //Consultas.ConsultandoDepartametos();

            //StoreProcedure.CriarStoreProcedure();
            //StoreProcedure.InserirDadosViaProcedure();
            //StoreProcedure.CriarStoreProcedureDeConsulta();
            //StoreProcedure.ConsultarDadosViaProcedure();

            //InfraEstrutura.HabilitarBatchSize();
            //InfraEstrutura.TempoComandoGeral();          

            //Relacionamentos.ConversorCustomizado();
            //Relacionamentos.TrabalhandoComPropriedadesSombra();
            //Relacionamentos.TiposDePropriedades();
            //Relacionamentos.Relacionamento1x1();
            //Relacionamentos.Relacionamento1xN();
            //Relacionamentos.RelacionamentoNxN();
            //Relacionamentos.CampoDeApoio();
            //Relacionamentos.ExemploTabelasPorHeranca();
            //Relacionamentos.PacoteDePropriedade();

            //Transacoes.ComportamentoPadrao();
            //Transacoes.GerenciandoTransacaoManualmente();
            //Transacoes.RevertendoTransacao();
            //Transacoes.SalvandoPontoTransacao();

            //SetUp();
            //PerformanceConsultas.ConsultaRastreada();
            //PerformanceConsultas.ConsultaNaoRastreada();
            //PerformanceConsultas.ConsultaComResolucaoDeIdentidade();
            //PerformanceConsultas.ConsultaProjetadaRastreada();
            //PerformanceConsultas.Inserir_200_departamentos_com_1mb();
            //PerformanceConsultas.ConsultaProjetada();
            //SetUp();

            //Funcoes.FuncaoDeDatas();
            //Funcoes.FuncaoLike();
            //Funcoes.FuncaoDataLenght();
            //Funcoes.FuncaoProperty();
            //Funcoes.FuncaoCollate();
            Funcoes.TesteIntercepcao();

        }

        private static void SetUp()
        {
            //using var db = new MeuContexto();
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Departamentos.Add(new Departamento
            {
                Descricao = "Departamento de teste",
                Ativo = true,
                Funcionarios = Enumerable.Range(1, 10).Select(p => new Funcionario
                {
                    CPF = p.ToString().PadLeft(11, '0'),
                    Nome = $"Funcionario {p}",
                    RG = p.ToString()
                }).ToList()
            });

            db.SaveChanges();

            var departamento = db.Departamentos.ToList();

            departamento.ForEach(d =>
            {
                Console.WriteLine($"Departamento: {d.Descricao}");

                foreach (var funcionario in d.Funcionarios)
                {
                    Console.WriteLine($"\t{funcionario.Id} - {funcionario.Nome}");
                }
            });
        }


    }
}
