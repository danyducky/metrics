using Metrics.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metrics.DataLayer
{
    public class MetricsContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MetricsContext(DbContextOptions<MetricsContext> options) :base(options) { }
    }
}