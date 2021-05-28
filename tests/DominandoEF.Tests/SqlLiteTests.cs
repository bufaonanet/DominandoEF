using DominandoEF.Tests.Data;
using DominandoEF.Tests.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace DominandoEF.Tests
{
    public class SqlLiteTests
    {


        [Theory(DisplayName = "Teste com mais registros")]
        [Trait("Categoria", "Departamento")]
        [InlineData("Tecnologia")]
        [InlineData("Financeiro")]
        [InlineData("Departamento pessoal")]
        public void Deve_inserir_e_consultar_um_departamento(string descricao)
        {
            //Arrange
            var departamento = new Departamento
            {
                Descricao = descricao,
                DataCadstro = DateTime.Now
            };

            //Setup
            var context = CreateContext();
            context.Database.EnsureCreated();
            context.Departamentos.Add(departamento);

            //Act
            var inserido = context.SaveChanges();
            departamento = context.Departamentos.FirstOrDefault(p => p.Descricao == descricao);

            //Assert
            Assert.Equal(1, inserido);
            Assert.Equal(descricao, departamento.Descricao);
        }

        private ApplicationContext CreateContext()
        {
            var conexao = new SqliteConnection("Datasource=:memory:");
            conexao.Open();

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                //.UseSqlite("Datasource=:memory:")
                .UseSqlite(conexao)
                .Options
                ;

            return new ApplicationContext(options);
        }

    }
}
