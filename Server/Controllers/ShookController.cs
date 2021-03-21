using System;
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
        public ICollection<Shook> Get()
        {
            return _dbContext.Shooks.ToListAsync().Result;
        }

        [HttpGet]
        [Route("{userId}")]
        public ICollection<Shook> GetShooksOfUser(int userId)
        {
            var user = _dbContext.Users.SingleAsync(u => u.Id == userId).Result;

            var shooksOfUser = new List<Shook>();
            var allShooks = _dbContext.Shooks
                .Include(s => s.Member)
                .ToListAsync()
                .Result;

            foreach (Shook shook in allShooks)
            {
                if (shook.Member.Contains(user))
                {
                    shooksOfUser.Add(shook);
                }
            }

            return shooksOfUser;
        }
    }
}
