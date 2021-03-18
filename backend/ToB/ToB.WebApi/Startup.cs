using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ToB.Common.DB;
using ToB.PriorityToDo.Contracts;
using ToB.PriorityToDo.DB;
using ToB.PriorityToDo.Objectives;
using ToB.PriorityToDo.Projects;

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

            services.AddDbContext<Context>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PriorityToDoContext")),
                ServiceLifetime.Singleton);

            services.AddTransient<ICrud<int, PriorityToDo.DB.Project>>(_ => new DbCrud<Context, PriorityToDo.DB.Project>(
                _.GetRequiredService<Context>(),
                _ => _.Projects));
            
            services.AddTransient<ICrud<int, Objective>>(_ => new DbCrud<Context, Objective>(
                _.GetRequiredService<Context>(),
                _ => _.Objectives));

            services.AddSingleton<IProjectService>(_ => new PriorityToDo.Projects.Service(
                _.GetRequiredService<ICrud<int, PriorityToDo.DB.Project>>(),
                1,
                (projects, root) => new Trees(projects).Build(root)));

            services.AddTransient<IMeasure>(_ => new Measure(1024));

            services.AddTransient<IBalancedTree<int>, BalancedTree<int>>();
            
            services.AddTransient<IProjects>(_ => new Projects(
                _.GetRequiredService<Context>(),
                id => PriorityToDo.Objectives.Project.Create(
                    project: id,
                    storage: _.GetRequiredService<ICrud<int, Objective>>(),
                    createTree: _.GetRequiredService<IBalancedTree<int>>)));

            services.AddSingleton<IObjectiveService, PriorityToDo.Objectives.Service>();

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
