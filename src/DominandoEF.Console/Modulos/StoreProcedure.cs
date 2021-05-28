using DominandoEF.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEF.Modulos
{
    public static class StoreProcedure
    {
        public static void ConsultarDadosViaProcedure()
        {
            using var db = new ApplicationContext();

            var dep = new SqlParameter("@parametro", "teste");

            var departamentos = db.Departamentos
                //.FromSqlRaw("EXECUTE CONSULTAR_DEPARTAMENTO {0}", dep)
                .FromSqlInterpolated($"EXECUTE CONSULTAR_DEPARTAMENTO {dep}")
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");
            }
        }

        public static void InserirDadosViaProcedure()
        {
            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw("EXECUTE CRIAR_DEPARTAMENTO {0}, {1}", "Departamento via procedure 2", false);
        }

        public static void CriarStoreProcedureDeConsulta()
        {
            var criarProcedure = @"
            CREATE PROCEDURE CONSULTAR_DEPARTAMENTO
	            @Descricao AS varchar(150)
            AS
            BEGIN	
	            SELECT * FROM Departamentos WHERE Descricao like @Descricao + '%'
            END";

            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw(criarProcedure);
        }

        public static void CriarStoreProcedure()
        {
            var criarDepartamento = @"
            CREATE PROCEDURE CRIAR_DEPARTAMENTO
	            @Descricao AS varchar(150),
	            @Ativo AS bit
            AS
            BEGIN	
	            insert into Departamentos(Descricao,Ativo,Excluido)
	            values(@Descricao,@Ativo,0)   
            END";

            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw(criarDepartamento);
        }
    }
}
