using Microsoft.EntityFrameworkCore;
using rate_api.DataAccess.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace rate_api.DataAccess.Models
{
    public class RatesContext : DbContext
    {
        private static IConfiguration Configuration { get; set; }
        // public RatesContext(DbContextOptions<RatesContext> options)
        //     : base(options)
        // { }

        public DbSet<Rate> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetConnectionString());
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration["RatesDBConnectionString"];
        }
    }
}