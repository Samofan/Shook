using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Login;
using Server.Database;
using Server.Models;

namespace Server.Controllers
{
    /// <summary>
    /// A controller for managing login and distributing JSON Web Tokens.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<LoginController> _logger;

        /// <summary>
        /// DbContext.
        /// </summary>
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Configuration for reading the appsettings.json.
        /// </summary>
        private readonly IConfiguration _configuration;

        public LoginController(ILogger<LoginController> logger,
            ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        /// <summary>
        /// POST method for checking credentials. A <see cref="LoginPackage"/>
        /// is handed over if the credentials are correct. The <see cref="LoginPackage"/>
        /// is empty of the credentials are not correct.
        /// </summary>
        /// <param name="user">Username and password inside a <see cref="User"/>
        /// object.</param>
        /// <returns>A <see cref="LoginPackage"/>.</returns>
        [HttpPost]
        public LoginPackage Login([FromBody] User user)
        {
            try
            {
                var userDto = CheckCredentials(user.Username, user.Password);
                return CreateToken(userDto);
            }
            catch (InvalidOperationException)
            {
                return new LoginPackage
                {
                    Data = null
                };
            }            
        }

        /// <summary>
        /// Check credentials with username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>A <see cref="UserDto"/> object if the credentials are valid.</returns>
        private UserDto CheckCredentials(string username, string password)
        {
            try
            {
                return _dbContext.Users
                    .Where(u => u.Username.Equals(username))
                    .Where(u => u.Password.Equals(password))
                    .Single();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException();
            }            
        }

        /// <summary>
        /// Creates a JSON Web Token.
        /// </summary>
        /// <param name="userDto">A <see cref="UserDto"/> object with username.</param>
        /// <returns>A <see cref="LoginPackage"/> with a JSON Web Token.</returns>
        private LoginPackage CreateToken(UserDto userDto)
        {
            var key = _configuration.GetValue<string>("JwtAuthorization:key");
            var issuer = _configuration.GetValue<string>("JwtAuthorization:issuer");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("valid", "1"),
                new Claim("userid", userDto.Id.ToString()),
                new Claim("name", userDto.Username)
            };
            
            var token = new JwtSecurityToken(issuer,   
                            issuer,
                            permClaims,
                            expires: DateTime.Now.AddDays(7),
                            signingCredentials: credentials);

            var loginPackage = new LoginPackage
            {
                Data = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return loginPackage;
        }
    }
}
