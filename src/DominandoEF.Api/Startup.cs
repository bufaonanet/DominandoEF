using DominandoEF.Api.Data;
using DominandoEF.Api.Data.Repository;
using DominandoEF.Api.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace DominandoEF.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DominandoEF.Api", Version = "v1" });
            });

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MSSQLLocalDB"));
            });

            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DominandoEF.Api v1"));
            }

            InicializaDb(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InicializaDb(IApplicationBuilder app)
        {
            using var db = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ApplicationContext>();

            if (db.Database.EnsureCreated())
            {
                db.Departamentos.AddRange(Enumerable.Range(1, 10).Select(p => new Departamento
                {
                    Descricao = $"Departamento {p}",
                    Colaboradores = Enumerable.Range(1, 10).Select(x => new Colaborador
                    {
                        Nome = $"Colaborador {x}/{p}"
                    }).ToList()
                }));

                db.SaveChanges();
            }
        }
    }
}
