using DominandoEF.MultiTenant.Data;
using DominandoEF.MultiTenant.Domain;
using DominandoEF.MultiTenant.Extensions;
using DominandoEF.MultiTenant.Middlewares;
using DominandoEF.MultiTenant.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DominandoEF.MultiTenant
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
            services.AddScoped<TenantData>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DominandoEF.MultiTenant", Version = "v1" });
            });

            //1º estratégia
            //services.AddDbContext<ApplicationContext>(p => p
            //    .UseSqlServer("Data source=(localdb)\\mssqllocaldb; Initial Catalog=Tenant; Integrated Security=true;")
            //    .LogTo(Console.WriteLine)
            //    .EnableSensitiveDataLogging());


            //2º estratégia
            services.AddHttpContextAccessor();
            services.AddScoped<ApplicationContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

                var httpcontext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                var tenantId = httpcontext?.GetTenantId();

                var connectionString = Configuration.GetConnectionString("custom").Replace("_DATABASE_",tenantId);

                optionsBuilder
                    .UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging();

                return new ApplicationContext(optionsBuilder.Options);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DominandoEF.MultiTenant v1"));
            }

            //DatabaseInitialize(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseMiddleware<TenantMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //private void DatabaseInitialize(IApplicationBuilder app)
        //{
        //    using var db = app.ApplicationServices
        //        .CreateScope()
        //        .ServiceProvider
        //        .GetRequiredService<ApplicationContext>();

        //    db.Database.EnsureDeleted();
        //    db.Database.EnsureCreated();

        //    for (int i = 0; i < 5; i++)
        //    {
        //        db.People.Add(new Person { Name = $"Person {i}" });
        //        db.Products.Add(new Product { Description = $"Product {i}" });
        //    }

        //    db.SaveChanges();
        //}
    }
}
