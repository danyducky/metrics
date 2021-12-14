using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Metrics.DataLayer
{
    public class MetricsContextDesignTime : IDesignTimeDbContextFactory<MetricsContext>
    {
        public MetricsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MetricsContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=metrics;Username=postgres;Password=1");

            return new MetricsContext(optionsBuilder.Options);
        }
    }
}