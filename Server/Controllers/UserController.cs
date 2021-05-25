using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Server.Database;
using Server.Models;

namespace Server.Controllers
{
    /// <summary>
    /// Controller class for managing users.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// DbContext.
        /// </summary>
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="dbContext">DbContext.</param>
        public UserController(ILogger<UserController> logger,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of all users in the database.</returns>
        [HttpGet]
        public ICollection<User> Get()
        {
            var users = new List<User>();
            var userDtos = _dbContext.Users.ToListAsync().Result;

            foreach (UserDto userDto in userDtos)
            {
                users.Add(new User(userDto));
            }

            return users;
        }

        /// <summary>
        /// Searches a specific user depending on the input. Only one parameter
        /// is mandatory.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="username">The username.</param>
        /// <returns>The searched user.</returns>
        [HttpGet]
        [Route("search")]
        public User GetSpecificUser([FromQuery] int userId, [FromQuery] string username)
        {
            return userId == 0 ? GetUserByUsername(username) : GetUserById(userId);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public void CreateUser(User user)
        {
            if (CheckUser(user))
            {
                var userDto = new UserDto
                {
                    Email = user.Email,
                    Username = user.Username,
                    Password = user.Password,
                    ProfilePicture = user.ProfilePicture
                };

                _dbContext.Users.Add(userDto);
                _dbContext.SaveChanges();

            }
        }

        private bool CheckUser(User user)
        {
            return true;
        }

        /// <summary>
        /// Gets a specific user depending on their id.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>A specific user depending on their id.</returns>
        private User GetUserById(int userId)
        {
           return _dbContext.GetUserById(userId);
        }

        /// <summary>
        /// Gets a specific user depending on their username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A specific user depending on their username.</returns>
        private User GetUserByUsername(string username)
        {
            return _dbContext.GetUserByUsername(username);
        }
    }
}
