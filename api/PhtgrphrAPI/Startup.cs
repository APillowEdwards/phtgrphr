using PhtgrphrAPI.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Repositories;
using PhtgrphrAPI.FileManagers;

namespace PhtgrphrAPI
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
            services.AddDbContext<PhtgrphrContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("db")));

            services.AddCors();

            services.AddTransient<IGalleryRepository, GalleryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IGalleryLogic, GalleryLogic>();
            services.AddTransient<IStatsLogic, StatsLogic>();
            services.AddTransient<IUserLogic, UserLogic>();

            // File Manager
            if (Configuration.GetValue<string>("FileManager") == "AzureBlob")
            {
                services.AddTransient<IFileManager, AzureBlobFileManager>();
            }
            else if (Configuration.GetValue<string>("FileManager") == "AzureFiles")
            {
                services.AddTransient<IFileManager, AzureFilesManager>();
            }
            else if (Configuration.GetValue<string>("FileManager") == "Local")
            {
                services.AddTransient<IFileManager, LocalFileManager>();
            } 
            else
            {
                throw new Exception("File manager not set");
            }

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseCors(options => options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
