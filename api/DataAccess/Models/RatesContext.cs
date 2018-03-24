using Microsoft.EntityFrameworkCore;
using rate_api.DataAccess.Models;
using System.Collections.Generic;

namespace rate_api.DataAccess.Models
{
    public class RatesContext : DbContext
    {
        public RatesContext(DbContextOptions<RatesContext> options)
            : base(options)
        { }

        public DbSet<Rate> Rates { get; set; }
    }
}