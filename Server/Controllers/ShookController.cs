using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Server.Database;
using Microsoft.AspNetCore.Http;
using Server.Utilities;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ShookController
    {
        private readonly ILogger<ShookController> _logger;

        private readonly ApplicationDbContext _dbContext;

        private readonly IHttpContextAccessor _httpContext;

        private readonly IJwtUtility _jwtUtility;

        public ShookController(ILogger<ShookController> logger,
            ApplicationDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _httpContext = httpContext;
            _jwtUtility = new JwtUtilityImpl(httpContext.HttpContext);
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

        [HttpPost]
        [Route("create")]
        public void CreateShook(Shook shook)
        {
            if (CheckShook(shook))
            {
                var shookDto = new ShookDto
                {
                    Title = shook.Title,
                    Description = shook.Description,
                    Creator = _dbContext.GetUserDtoByUsername(_jwtUtility.GetUsername()),
                    StartTime = shook.StartTime,
                    EndTime = shook.EndTime
                };

                shookDto.ShookUsers.Add(new UserShookDto
                {
                    Shook = shookDto,
                    ShookDtoId = shookDto.Id,
                    User = shookDto.Creator,
                    UserDtoId = shookDto.Creator.Id
                });

                _dbContext.Shooks.Add(shookDto);
                _dbContext.SaveChanges();
            }
        }

        [HttpPost]
        [Route("join")]
        public void Join(Shook shook)
        {
            var username = _jwtUtility.GetUsername();
            var userDto = _dbContext.GetUserDtoByUsername(username);
            var shookDto = _dbContext.GetShookDtoByShook(shook);

            UserShookDto userShookDto = new UserShookDto
            {
                Shook = shookDto,
                ShookDtoId = shookDto.Id,
                User = userDto,
                UserDtoId = userDto.Id
            };

            shookDto.ShookUsers.Add(userShookDto);
            _dbContext.Shooks.Update(shookDto);
            _dbContext.SaveChanges();
        }

        private bool CheckShook(Shook shook)
        {
            return true;
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
