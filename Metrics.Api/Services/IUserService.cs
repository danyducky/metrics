using System;
using System.Collections;
using System.Collections.Generic;
using Metrics.Api.Models.User;
using Metrics.DataLayer.Entities;

namespace Metrics.Api.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        IEnumerable<User> CreateRange(UserCreateModel model);
        void Update(Guid id, UserDateModel model);
        void Remove(Guid id);
    }
}