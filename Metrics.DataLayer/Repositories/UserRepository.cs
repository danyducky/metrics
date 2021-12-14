using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metrics.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metrics.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MetricsContext _context;

        public UserRepository(MetricsContext context)
        {
            _context = context;
        }
        
        public IQueryable<User> Get()
        {
            return _context.Users.AsNoTracking();
        }

        public User Find(Guid id)
        {
            return _context.Users.Find(id);
        }

        public void AddRange(IEnumerable<User> users)
        {
            _context.Users.AddRange(users);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}