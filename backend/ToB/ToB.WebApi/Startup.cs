using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ToB.Common.DB;
using ToB.PriorityToDo.DB;
using ToB.PriorityToDo.Objectives;
using ToB.PriorityToDo.Projects;

using ToB.WebApi.DB;
using ToB.WebApi.Interfaces;
using ToB.WebApi.Services;
using IService = ToB.PriorityToDo.Objectives.IService;
using Project = ToB.PriorityToDo.Objectives.Project;
using Service = ToB.PriorityToDo.Objectives.Service;

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
                options.UseNpgsql(Configuration.GetConnectionString("PriorityToDoContext")));

            services.AddTransient<ICrud<int, PriorityToDo.DB.Project>>(_ => new DbCrud<Context, PriorityToDo.DB.Project>(
                _.GetRequiredService<Context>(),
                _ => _.Projects));

            services.AddTransient<PriorityToDo.Projects.IService>(_ => new PriorityToDo.Projects.Service(
                _.GetRequiredService<ICrud<int, PriorityToDo.DB.Project>>(),
                1,
                (projects, root) => new Trees(projects).Build(root)));

            services.AddTransient<IMeasure>(_ => new Measure(1024));
            
            services.AddTransient<IProjects>(_ => new Projects(
                _.GetRequiredService<Context>(),
                id => Project.Create(
                    id,
                    _.GetRequiredService<IMeasure>(),
                    _.GetRequiredService<ICrud<int, Objective>>(),
                    () => throw new NotImplementedException()))); //TODO
            
            services.AddTransient<IService>(_ => new Service(
                _.GetRequiredService<IProjects>(),
                new IntSequence().Next));

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
