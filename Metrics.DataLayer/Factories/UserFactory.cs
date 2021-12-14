using System;
using Metrics.DataLayer.Entities;

namespace Metrics.DataLayer.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(DateTime registrationDate, DateTime lastActivityDate)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                RegistrationDate = registrationDate,
                LastActivityDate = lastActivityDate
            };
        }
    }
}