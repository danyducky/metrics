using System;
using System.Collections.Generic;
using System.Linq;
using Metrics.DataLayer.Entities;

namespace Metrics.DataLayer.Repositories
{
    public interface IUserRepository
    {
        public IQueryable<User> Get();
        public User Find(Guid id);
        public void AddRange(IEnumerable<User> users);
        public void Remove(User user);
        public void Update(User user);
        public void SaveChanges();
    }
}