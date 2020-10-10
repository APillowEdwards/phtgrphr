using Microsoft.EntityFrameworkCore;
using PhtgrphrAPI.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhtgrphrAPIFunctions
{
    class FunctionContext : PhtgrphrContext
    {
        public FunctionContext(DbContextOptions<PhtgrphrContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            optionsBuilder.UseLazyLoadingProxies()
                .UseMySQL(connectionString);
        }
    }
}
