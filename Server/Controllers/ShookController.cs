using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Server.Database;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShookController
    {
        private readonly ILogger<ShookController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public ShookController(ILogger<ShookController> logger,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ICollection<ShookDto> Get()
        {
            return _dbContext.Shooks.ToListAsync().Result;
        }

        [HttpGet]
        [Route("ofUser")]
        public ICollection<Shook> GetShooksOfUser([FromQuery] User user,
            [FromQuery] int userId)
        {
            return userId == 0 ? GetShooksOfUserByUser(user) : GetShooksOfUserById(userId);
        }

        private ICollection<Shook> GetShooksOfUserByUser(User user)
        {
            return _dbContext.GetShooksOfUserByUser(user);
        }

        private ICollection<Shook> GetShooksOfUserById(int userId)
        {
            return _dbContext.GetShooksOfUserById(userId);
        }
    }
}
