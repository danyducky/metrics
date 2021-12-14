using System;
using System.Linq;
using Metrics.DataLayer.Repositories;

namespace Metrics.Api.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly IUserRepository _userRepository;

        public MetricsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public double CalculateRollingRetention(int days)
        {
            var query = _userRepository.Get();

            var activeUsers = query
                .Where(x => (x.LastActivityDate - x.RegistrationDate).Days + 1 >= days)
                .ToList();
    
            var newUsers = query
                .Where(x => x.RegistrationDate <= DateTime.Now.AddDays(-days))
                .ToList();

            return activeUsers.Count > 0 && newUsers.Count > 0
                ? ((double) activeUsers.Count / newUsers.Count) * 100
                : default;
        }
    }
}