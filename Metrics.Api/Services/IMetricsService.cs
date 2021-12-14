namespace Metrics.Api.Services
{
    public interface IMetricsService
    {
        public double CalculateRollingRetention(int days);
    }
}