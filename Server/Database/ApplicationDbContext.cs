using System;
using Microsoft.EntityFrameworkCore;
using Model;
using Server.Models;

namespace Server.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Shook> Shooks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary key of user.
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Some ignored properties for user.
            //modelBuilder.Entity<User>()
            //    .Ignore(u => u.CreatedShooks)
            //    .Ignore(u => u.WonShooks);

            // Primary key for shook.
            modelBuilder.Entity<Shook>()
                .HasKey(s => s.Id);

            // Foreign key to the creator.
            /*modelBuilder.Entity<Shook>()
                .HasOne(u => u.Creator);

            // Foreign key to the winner.
            modelBuilder.Entity<Shook>()
                .HasOne(u => u.Winner);*/

            // Many to many relationship betwenn user and shook.
            modelBuilder.Entity<Shook>()
                .HasMany(u => u.Member)
                .WithMany(u => u.Shooks);          
        }
    }
}
