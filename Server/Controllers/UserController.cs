using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Server.Database;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly ILogger<UserController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public UserController(ILogger<UserController> logger,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ICollection<User> Get()
        { 
            return _dbContext.Users.ToListAsync().Result;
        }

        [HttpGet]
        [Route("{userId}")]
        public User GetUser(int userId)
        {
            return _dbContext.Users.SingleAsync(u => u.Id == userId).Result;
        }

        private List<User> GetDummyUsers()
        {
            var users = new List<User>();

            users.Add(new User()
            {
                Email = "dinter.florian@googlemail.com",
                Username = "Samofan",
                Password = "Test123",
                ProfilePicture = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/ULi-Logo.svg/2880px-ULi-Logo.svg.png")
            });

            users.Add(new User()
            {
                Email = "leajjohanna@mail.com",
                Username = "leajjohanna",
                Password = "florianIsKuschelchen",
                ProfilePicture = null
            });

            return users;
        }

        private Shook CreateShook()
        {
            var shook = new Shook()
            {
                Title = "Test",
                Description = "Database test",
                CreatorId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.MaxValue,
                WinnerId = 0
            };

            shook.Member.Add(_dbContext.Users.SingleAsync(u => u.Id == 1).Result);
            shook.Member.Add(_dbContext.Users.SingleAsync(u => u.Id == 2).Result);

            return shook;
        }
    }
}
