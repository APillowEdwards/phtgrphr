using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhtgrphrAPI.DbContexts;
using PhtgrphrAPI.FileManagers;
using PhtgrphrAPI.Logic;
using PhtgrphrAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhtgrphrAPIFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            builder.Services.AddDbContext<PhtgrphrContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseMySQL(connectionString));

            builder.Services.AddTransient<IGalleryLogic, GalleryLogic>();
            builder.Services.AddTransient<IUserLogic, UserLogic>();

            builder.Services.AddTransient<IGalleryRepository, GalleryRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddTransient<IFileManager, LocalFileManager>();
        }
    }
}
