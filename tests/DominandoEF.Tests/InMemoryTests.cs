using DominandoEF.Tests.Data;
using DominandoEF.Tests.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace DominandoEF.Tests
{
    public class InMemoryTests
    {   

        [Fact(DisplayName ="Inserindo com sucesso")]
        [Trait("Categoria", "Departamento")]
        public void Inserindo_com_sucesso()
        {
            //Arrange
            var departamento = new Departamento
            {
                Descricao = "Tecnologia",
                DataCadstro = DateTime.Now
            };

            //Setup
            var context = CreateContext();
            context.Departamentos.Add(departamento);

            //Act
            var inserido = context.SaveChanges();

            //Assert
            Assert.Equal(1, inserido);
        }

        private ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("InMemoryDataBase").Options;

            return new ApplicationContext(options);
        }

    }
}
