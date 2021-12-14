using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metrics.Api.Models.User;
using Metrics.DataLayer.Entities;
using Metrics.DataLayer.Factories;
using Metrics.DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Metrics.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public UserService(IUserRepository userRepository, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.Get().AsEnumerable();
        }

        public IEnumerable<User> CreateRange(UserCreateModel model)
        {
            var users = model.Dates
                .Select(x => _userFactory.Create(x.RegistrationDate, x.LastActivityDate));
            
            _userRepository.AddRange(users);
            _userRepository.SaveChanges();

            return users;
        }

        public void Update(Guid id, UserDateModel model)
        {
            var user = _userRepository.Find(id);

            _userRepository.Update(user);

            user.RegistrationDate = model.RegistrationDate;
            user.LastActivityDate = model.LastActivityDate;

            _userRepository.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var user = _userRepository.Find(id);
            
            _userRepository.Remove(user);
            _userRepository.SaveChanges();
        }
    }
}