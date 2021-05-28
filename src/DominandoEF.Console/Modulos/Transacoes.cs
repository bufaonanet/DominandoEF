using DominandoEF.Data;
using DominandoEF.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Modulos
{
    public static class Transacoes
    {
        public static void SalvandoPontoTransacao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var transacao = db.Database.BeginTransaction();

                try
                {
                    var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                    livro.Autor = "Bufão";
                    db.SaveChanges();

                    transacao.CreateSavepoint("desfazer_apenas_inserções");

                    db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Testando inserção",
                        Autor = "Teste"
                    });

                    db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Testando inserção",
                        Autor = "Provocando Exception".PadLeft(16, '*')
                    });

                    db.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception)
                {
                    transacao.RollbackToSavepoint("desfazer_apenas_inserções");
                    transacao.Commit();
                }
            }
        }

        public static void RevertendoTransacao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var transacao = db.Database.BeginTransaction();

                try
                {
                    var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                    livro.Autor = "Bufão";

                    db.SaveChanges();

                    db.Livros.Add(
                        new Livro
                        {
                            Titulo = "Usando Sql Server",
                            Autor = "Zezinho DA SILVA ".PadLeft(16, '*')
                        }
                    );

                    db.SaveChanges();

                    transacao.Commit();

                }
                catch (Exception)
                {

                    transacao.Rollback();
                }
            }
        }

        public static void GerenciandoTransacaoManualmente()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var transacao = db.Database.BeginTransaction();

                var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                livro.Autor = "Douglas";

                db.SaveChanges();

                db.Livros.Add(
                    new Livro { Titulo = "Usando Sql Server", Autor = "Bufão" }
                );

                db.SaveChanges();

                transacao.Commit();
            }
        }

        public static void ComportamentoPadrao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                livro.Autor = "Douglas";

                db.Livros.Add(
                    new Livro { Titulo = "Usando Sql Server", Autor = "Bufão" }
                );

                db.SaveChanges();
            }
        }

        private static void CadastrarLivro()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Livro { Titulo = "Dominando o Entity Framework", Autor = "Douglas" }
                );

                db.SaveChanges();
            }
        }
    }
}
