using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskManagement.API.Models;

namespace TaskManagement.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Tasks and Users
        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                Id = 1,
                Title = "Prepare project structure",
                Description = "Setup Clean Architecture folders.",
                IsCompleted = false,
                CreatedAt = new DateTime(2024, 06, 01, 12, 0, 0, DateTimeKind.Utc)
            },
            new TaskItem
            {
                Id = 2,
                Title = "Implement TaskService",
                Description = "Add business logic for Tasks.",
                IsCompleted = false,
                CreatedAt = new DateTime(2024, 06, 02, 9, 30, 0, DateTimeKind.Utc)
            },
            new TaskItem
            {
                Id = 3,
                Title = "Seed initial data",
                Description = "Add data seeding for Tasks and Users.",
                IsCompleted = true,
                CreatedAt = new DateTime(2024, 06, 03, 15, 45, 0, DateTimeKind.Utc)
            }
        );


        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "password"
            }
        );
    }

}
