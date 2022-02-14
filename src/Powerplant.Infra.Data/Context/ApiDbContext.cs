using Microsoft.EntityFrameworkCore;
using Powerplant.Core.Domain.Model;
using Powerplant.Infra.Data.EntityConfig;

namespace Powerplant.Infra.Data.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<ParamModel> Params { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParamModel>(new ParamConfiguration().Configure);
        }
    }
}
