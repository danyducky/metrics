using Metrics.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.Api.Controllers
{
    [Route("api/[controller]")]
    public class MetricsController : Controller
    {
        private readonly IMetricsService _metricsService;

        public MetricsController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }
        
        /// <summary>
        /// Расчёт повторяющегося удержания
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        [HttpGet("retention/{days:int}")]
        public IActionResult RollingRetention(int days)
        {
            var result = _metricsService.CalculateRollingRetention(days);
            return Ok(result);
        }
    }
}