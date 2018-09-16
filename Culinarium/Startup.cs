using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Culinarium.Repository.ApplicationDbContext;
using Culinarium.Repository.Interfaces;
using Culinarium.Repository.Repositories;
using Culinarium.Services.Interfaces;
using Culinarium.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Examples;

namespace Culinarium
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

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Culinarium;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            services.AddCors();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Title = "Culinarium Api",
                    Description = "swagger for culinarium",
                });
            });

            services.AddSingleton<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<IMapperManager, MapperManager>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddAutoMapper();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger API"));
        }
    }
}
