using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Student>().ToTable("Students");

            modelBuilder.Entity<User>()
            .HasIndex(u => u.UserLogin)
            .IsUnique();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
