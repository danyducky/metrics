using System;
using Metrics.DataLayer.Entities;

namespace Metrics.DataLayer.Factories
{
    public interface IUserFactory
    {
        public User Create(DateTime registrationDate, DateTime lastActivityDate);
    }
}