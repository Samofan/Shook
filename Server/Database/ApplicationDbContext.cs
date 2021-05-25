using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Database
{
    /// <summary>
    /// Handles interactions with the database.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public DbSet<UserDto> Users { get; set; }

        public DbSet<ShookDto> Shooks { get; set; }

        public DbSet<UserShookDto> UserShooks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ILogger<ApplicationDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary key of user.
            modelBuilder.Entity<UserDto>()
                .HasKey(u => u.Id);

            // Primary key for shook.
            modelBuilder.Entity<ShookDto>()
                .HasKey(s => s.Id);

            // Foreign key to the creator.
            modelBuilder.Entity<ShookDto>()
                .HasOne(u => u.Creator);

            // Foreign key to the winner.
            modelBuilder.Entity<ShookDto>()
                .HasOne(u => u.Winner);

            modelBuilder.Entity<UserShookDto>().HasKey(us => new { us.UserDtoId, us.ShookDtoId });

            modelBuilder.Entity<UserShookDto>()
                .HasOne(u => u.User)
                .WithMany(us => us.UserShooks)
                .HasForeignKey(us => us.UserDtoId);

            modelBuilder.Entity<UserShookDto>()
                .HasOne(s => s.Shook)
                .WithMany(us => us.ShookUsers)
                .HasForeignKey(us => us.ShookDtoId);
        }

        /// <summary>
        /// Gets a <see cref="User"/> by its username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>A <see cref="User"/>.</returns>
        public User GetUserByUsername(string username)
        {
            // TODO: Maybe out source this code to another Repository Class?
            var user = new User();

            try
            {
                var userDto = Users
                    .SingleAsync(u => u.Username.Equals(username))
                    .Result;
                user = new User(userDto);
            }
            catch (AggregateException)
            {
                _logger.LogError("User with username {} could not be found",
                    username);
            }

            return user;
        }

        /// <summary>
        /// Gets a <see cref="User"/> by its id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A <see cref="User"/>.</returns>
        public User GetUserById(int userId)
        {
            var user = new User();

            try
            {
                var userDto = Users
                    .SingleAsync(u => u.Id == userId)
                    .Result;
                user = new User(userDto);
            }
            catch (AggregateException)
            {
                _logger.LogError("User with id {} could not be found", userId);
            }

            return user;
        }

        public UserDto GetUserDtoByUser(User user)
        {
            return GetUserDtoByUsername(user.Username);
        }

        public UserDto GetUserDtoByUsername(string username)
        {
            var userDto = Users
                .SingleAsync(u => u.Username.Equals(username))
                .Result;

            return userDto;
        }

        public ShookDto GetShookDtoByShook(Shook shook)
        {
            return Shooks.SingleAsync(s => s.Id == shook.Id).Result;
        }

        /// <summary>
        /// Gets all <see cref="Shook"/> that are related to this user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A list of <see cref="Shook"/>.</returns>
        public List<Shook> GetShooksOfUserByUser(User user)
        {
            var shooksOfUser = new List<Shook>();
            var userDto = Users
                .SingleAsync(u => u.Username.Equals(user.Username))
                .Result;

            foreach (ShookDto shookDto in Shooks.Include(su => su.ShookUsers))
            {
                foreach (UserShookDto userShookDto in shookDto.ShookUsers)
                {
                    if (userShookDto.User == userDto)
                    {
                        shooksOfUser.Add(new Shook(userShookDto.Shook));
                    }
                }
            }

            return shooksOfUser;
        }

        /// <summary>
        /// Gets all <see cref="Shook"/> that are related to this user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A list of <see cref="Shook"/>.</returns>
        public List<Shook> GetShooksOfUserById(int userId)
        {
            var userDto = Users.SingleAsync(u => u.Id == userId).Result;
            return GetShooksOfUserByUser(new User(userDto));
        }
    }
}
