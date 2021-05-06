using Microsoft.EntityFrameworkCore;
using Model;
using Server.Models;
using System;
using System.Collections.Generic;

namespace Server.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }

        public DbSet<ShookDto> Shooks { get; set; }

        //public DbSet<UserShookDto> ShookUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

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

        internal List<Shook> GetShooksOfUser(UserDto userDto)
        {
            var returnList = new List<Shook>();

            foreach (ShookDto shookDto in Shooks.Include(su => su.ShookUsers))
            {
                foreach (UserShookDto userShookDto in shookDto.ShookUsers)
                {
                    if (userShookDto.User == userDto)
                    {
                        returnList.Add(new Shook(userShookDto.Shook));
                    }
                }
            }

            return returnList;
        }
    }
}
