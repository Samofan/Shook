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
            return CreateUsersList(_dbContext.Users.ToListAsync().Result);
        }

        [HttpGet]
        [Route("{userId}")]
        public UserDto GetUser(int userId)
        {
            return _dbContext.Users.SingleAsync(u => u.Id == userId).Result;
        }

        private List<User> CreateUsersList(ICollection<UserDto> userDtos)
        {
            var returnList = new List<User>();

            foreach (UserDto userDto in userDtos)
            {
                var user = new User(userDto);
                user.Shooks = _dbContext.GetShooksOfUser(userDto);
                returnList.Add(user);
            }

            return returnList;
        }

        private void CreateDummyData()
        {
            ShookDto shook = CreateShook();
            UserDto flo = GetDummyUsers()[0];
            UserDto lea = GetDummyUsers()[1];
            shook.Creator = flo;
            shook.Winner = lea;

            UserShookDto userShook1 = new UserShookDto();
            UserShookDto userShook2 = new UserShookDto();

            userShook1.User = flo;
            userShook1.Shook = shook;

            userShook2.User = lea;
            userShook2.Shook = shook;

            shook.ShookUsers.Add(userShook1);
            shook.ShookUsers.Add(userShook2);

            _dbContext.Users.Add(flo);
            _dbContext.Users.Add(lea);
            _dbContext.SaveChanges();

            _dbContext.Shooks.Add(shook);
            _dbContext.SaveChanges();
        }

        private List<UserDto> GetDummyUsers()
        {
            var users = new List<UserDto>();

            users.Add(new UserDto()
            {
                Email = "dinter.florian@googlemail.com",
                Username = "Samofan",
                Password = "Test123",
                ProfilePicture = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/ULi-Logo.svg/2880px-ULi-Logo.svg.png")
            });

            users.Add(new UserDto()
            {
                Email = "leajjohanna@mail.com",
                Username = "leajjohanna",
                Password = "florianIsKuschelchen",
                ProfilePicture = null
            });

            return users;
        }

        private ShookDto CreateShook()
        {
            var shook = new ShookDto()
            {
                Title = "Another Shook",
                Description = "Another Database test",
                StartTime = DateTime.Now,
                EndTime = DateTime.MaxValue,
            };

            return shook;
        }
    }
}
