using System;
using Metrics.Api.Models.User;
using Metrics.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// Список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Создание новых пользователей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] UserCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var users = _userService.CreateRange(model);

            return Ok(users);
        }
        
        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UserDateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _userService.Update(id, model);

            return Ok();
        }
        
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public IActionResult Remove(Guid id)
        {
            _userService.Remove(id);
            
            return Ok();
        }
    }
}