using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ToB.PriorityToDo;
using ToB.PriorityToDo.DB;

using ToB.WebApi.DB;
using ToB.WebApi.Interfaces;
using ToB.WebApi.Services;

namespace ToB.WebApi
{
    public class Startup
    {
        private const string Policy = "_specificOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy(Policy,
                    p => p.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            #region Randomizer
            
            services.AddDbContext<RandomizerContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("RandomizerContext")));
            
            services.AddTransient<IRandomizer>(_ => new Randomizer(new Random(DateTime.Now.Millisecond)));
            services.AddTransient<ISqlRequests<RandomizerContext>, SqlRequests<RandomizerContext>>();
            
            // services.AddTransient<IRegistries, EntityRegistries>();
            services.AddTransient<IRegistries, SqlRegistries>();
            
            services.AddTransient<IRandomRegistries, RandomRegistries>();
            
            #endregion

            #region PriorityToDo

            services.AddDbContext<ToB.PriorityToDo.DB.Context>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PriorityToDoContext")));

            services.AddTransient<IProjectService>(_ => new ProjectService(
                _.GetRequiredService<Context>(),
                1,
                (projects, root) => new Trees(projects).Build(root)));
            
            services.AddTransient<IObjectiveService, ObjectiveService>();

            #endregion
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(Policy);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
