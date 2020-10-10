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

[assembly: FunctionsStartup(typeof(PhtgrphrAPIFunctions.Startup))]

namespace PhtgrphrAPIFunctions
{
    public class Startup : FunctionsStartup
    {
        // This doesn't get run right now due to an issue with Functions, so using a workaround

        public override void Configure(IFunctionsHostBuilder builder)
        {
            //string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            //builder.Services.AddDbContext<PhtgrphrContext>(options =>
            //    options.UseLazyLoadingProxies()
            //        .UseMySQL(connectionString));

            //builder.Services.AddTransient<IGalleryRepository, GalleryRepository>();
            //builder.Services.AddTransient<IUserRepository, UserRepository>();

            //builder.Services.AddTransient<IGalleryLogic, GalleryLogic>();
            //builder.Services.AddTransient<IUserLogic, UserLogic>();

            //// File Manager
            //if (Environment.GetEnvironmentVariable("FileManager") == "Azure")
            //{
            //    builder.Services.AddTransient<IFileManager, AzureFilesManager>();
            //}
            //else if (Environment.GetEnvironmentVariable("FileManager") == "Local")
            //{
            //    builder.Services.AddTransient<IFileManager, LocalFileManager>();
            //}
            //else
            //{
            //    throw new Exception("File manager not set");
            //}
        }
    }
}
