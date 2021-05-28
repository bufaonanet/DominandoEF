using DominandoEF.Data;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Modulos
{
    public static class ManipularDB
    {
        public static void RecriarBanco()
        {
            //using var db = new ApplicationContext();
            using var db = new MeuContexto();
            var canConnect = db.Database.CanConnect();
            if (canConnect)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public static void EnsureCreated()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted(); //Deleta o banco
            db.Database.EnsureCreated(); //Cria o banco
        }

        public static void CriandoDBMultiploesContextos()
        {
            using var db1 = new ApplicationContext();
            using var db2 = new MeuContexto();

            db1.Database.EnsureCreated();
            db2.Database.EnsureCreated();

            var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();

            databaseCreator.CreateTables();
        }

        public static void HealthCheckBancoDeDados()
        {
            using var db = new ApplicationContext();
            var canConnect = db.Database.CanConnect();


            if (canConnect)
            {
                Console.WriteLine("Posso me conectar");
            }
            else
            {
                Console.WriteLine("Falha ao conectar");
            }

            #region ExemploVerificarConexao
            //try
            //{
            //    //1° maneira de verificar conexão
            //    var connection = db.Database.GetDbConnection();
            //    connection.Open();

            //    //2° Maneira de verificar
            //    db.Departamentos.Any();

            //    Console.WriteLine("Posso me conectar");
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Falha ao tentar conectar!");
            //}

            #endregion
        }

        public static void GerenciandoEstadoDaConexao(bool gerenciarEstadoConexao, int _count)
        {
            using var db = new ApplicationContext();
            var time = System.Diagnostics.Stopwatch.StartNew();

            var conexao = db.Database.GetDbConnection();
            conexao.StateChange += (_, __) => _count++;

            if (gerenciarEstadoConexao)
            {
                conexao.Open();
            }

            for (int i = 0; i < 200; i++)
            {
                db.Departamentos.AsNoTracking().Any();
            }

            time.Stop();
            Console.WriteLine($"Tempo: {time.Elapsed}, Gerenciado = {gerenciarEstadoConexao}, Contador: {_count}");

        }

        public static void ExecuteSql()
        {
            using var db = new ApplicationContext();

            //1° Opção de Execução de comandos sql
            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                //b.Database.GetDbConnection().Open();

                cmd.CommandText = "SELECT 1";
                cmd.ExecuteNonQuery();
            }

            //2° Opção de Execução de comandos sql
            var descricao = "TESTE";
            db.Database.ExecuteSqlRaw("UPDATE DEPARTAMENTOS SET descricao = {0} WHERE id = 1", descricao);

            //3° Opção de Execução de comandos sql
            descricao = "outro";
            db.Database.ExecuteSqlInterpolated($"UPDATE DEPARTAMENTOS SET descricao = {descricao} WHERE id = 1");

        }

        public static void SqlInjection()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted(); //Deleta o banco
            db.Database.EnsureCreated(); //Cria o banco

            db.Departamentos.AddRange(
                new Departamento { Descricao = "Departamento 1", Ativo = true },
                new Departamento { Descricao = "Departamento 2", Ativo = false }
            );
            db.SaveChanges();

            var descricao = "Departamento alterado";
            var condicao = "Departamento 1";
            db.Database.ExecuteSqlRaw("UPDATE departamentos SET descricao = {0} WHERE descricao = {1}", descricao, condicao);

            foreach (var d in db.Departamentos.AsNoTracking())
            {
                Console.WriteLine($"Id:{d.Id}, Descricao: {d.Descricao}");
            }

        }

        public static void ScriptGeradoBancoDeDados()
        {
            using var db = new MeuContexto();

            var script = db.Database.GenerateCreateScript();

            Console.WriteLine(script);
        }
        public static void ObterTodasMigracoes()
        {
            using var db = new ApplicationContext();

            var migracoes = db.Database.GetMigrations();

            Console.WriteLine($"Total de migrações: {migracoes.Count()}");

            foreach (var migracao in migracoes)
            {
                Console.WriteLine($"Migração: {migracao}");
            }
        }
        public static void MigracoesJaExecutadas()
        {
            using var db = new ApplicationContext();

            var migracoesPendentes = db.Database.GetAppliedMigrations();

            Console.WriteLine($"Total de migrações executuadas: {migracoesPendentes.Count()}");

            foreach (var migracao in migracoesPendentes)
            {
                Console.WriteLine($"Migração: {migracao}");
            }
        }
        public static void AplicarMigracoesPendentes()
        {
            using var db = new ApplicationContext();
            db.Database.Migrate();
        }
        public static void MigracoesPendentes()
        {
            using var db = new ApplicationContext();

            var migracoesPendentes = db.Database.GetPendingMigrations();

            Console.WriteLine($"Total de migrações pendetes: {migracoesPendentes.Count()}");

            foreach (var migracao in migracoesPendentes)
            {
                Console.WriteLine($"Migração: {migracao}");
            }
        }
    }
}
