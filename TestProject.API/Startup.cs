using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestProject.API.DTOs;
using TestProject.API.Exentions;
using TestProject.API.Filters;
using TestProject.Core.Repositories;
using TestProject.Core.Services;
using TestProject.Core.UnitOfWorks;
using TestProject.Data;
using TestProject.Data.Repositories;
using TestProject.Data.UnitOfWorks;
using TestProject.Service.Services;


namespace TestProject.API
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

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));

            services.AddScoped<NotFoundFilter>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConstr"].ToString(),a=> {

                    a.MigrationsAssembly("TestProject.Data");
                
                }); 
            });

            // request sýrasýnda clasýn ctorun da her uof interfacesi ile karþýlasýldýðýnda hepsi için bi nesne üreticek performans icin dah iyi birden fazla ihtiyac olsa bile ayný nesneyi kullanýcak
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers(i =>

                i.Filters.Add(new ValidationFilter())

            ) ;

                //apiye sen filterlarý konrol etme ben edeceðimdedim
            services.Configure<ApiBehaviorOptions>(options =>

            {
                options.SuppressModelStateInvalidFilter = true;
            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }




            //kendi extention methodum
            app.UseCustomException();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
