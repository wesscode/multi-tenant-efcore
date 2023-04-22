using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Multitenant.API.Data;
using Multitenant.API.Data.Interceptors;
using Multitenant.API.Data.ModelFactory;
using Multitenant.API.Domain;
using Multitenant.API.Middlewares;
using Multitenant.API.Provider;

namespace EFCore.Multitenant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<TenantData>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFCore.Multitenant", Version = "v1" });
            });

            /*
             * ESTRATÉGIA 1
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
                optionsBuilder
                    .UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Tenant99; Integrated Security=True;")
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging());
            */

            //services.AddScoped<StrategySchemaInterceptor>(); //para recuperar o intercepto com run time, preciso adicionar a instancia a um escopo.
            services.AddDbContext<ApplicationContext>((provider, optionsBuilder) =>
            {
                optionsBuilder
                    .UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Tenant99; Integrated Security=True;")
                    .LogTo(Console.WriteLine)
                    .ReplaceService<IModelCacheKeyFactory, StrategySchemaModelCacheKey>() //Fazendo replace SCHEMA com FactoryCacheKey
                    .EnableSensitiveDataLogging();

                //Fazendo replace SCHEMA com interceptor. Recuperado com interceptor em runtime, pois preciso do TenantData Preenchido.
                /*var interceptor = provider.GetRequiredService<StrategySchemaInterceptor>();
                optionsBuilder.AddInterceptors(interceptor);*/
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore.Multitenant v1"));
            }

            //DatabaseInitialize(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<TenantMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //Utilizado somente para teste de api inicial.
        //private void DatabaseInitialize(IApplicationBuilder app)
        //{
        //    using var db = app.ApplicationServices
        //        .CreateScope()
        //        .ServiceProvider
        //        .GetRequiredService<ApplicationContext>();

        //    db.Database.EnsureDeleted();
        //    db.Database.EnsureCreated();

        //    for (var i = 1; i <= 5; i++)
        //    {
        //        db.People.Add(new Person { Name = $"Person {i}" });
        //        db.Products.Add(new Product { Description = $"Product {i}" });
        //    }

        //    db.SaveChanges();
        //}
    }
}
