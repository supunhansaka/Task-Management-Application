using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Enums;
using TaskManagement.API.Models;
using TaskStatus = TaskManagement.API.Enums.TaskStatus;
using TaskPriority = TaskManagement.API.Enums.TaskPriority;

namespace TaskManagement.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskItem>()
        .Property(t => t.Status)
        .HasConversion<string>();

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Priority)
            .HasConversion<string>();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "password"
            }
        );

        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                Id = 1,
                Title = "Prepare project structure",
                Description = "Setup project to code.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 01, 12, 0, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 2,
                Title = "Implement TaskService",
                Description = "Add business logic for Tasks.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 02, 9, 30, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 3,
                Title = "Seed initial data",
                Description = "Add data seeding for Tasks and Users.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 03, 15, 45, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 4,
                Title = "Implement Authentication",
                Description = "Implement simple username/password authentication with cookies.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 04, 10, 15, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 5,
                Title = "Create Login Page UI",
                Description = "Develop login form with validation and SCSS styling.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 05, 14, 0, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 6,
                Title = "Add task list sorting",
                Description = "Implement sorting of tasks by title.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 06, 16, 20, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 7,
                Title = "Add task filtering",
                Description = "Implement search/filtering for task list.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 07, 11, 5, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 8,
                Title = "Build Dashboard Page",
                Description = "Display task list and task form side by side.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 08, 13, 50, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 9,
                Title = "Style Dashboard for Mobile",
                Description = "Make Dashboard responsive for mobile devices.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 09, 17, 40, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 10,
                Title = "Test Full Application",
                Description = "Test all user flows and fix any bugs.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 10, 9, 0, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 11,
                Title = "Implement Logout Functionality",
                Description = "Allow user to logout and clear session.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 11, 12, 30, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 12,
                Title = "Improve Form Validation",
                Description = "Add validation messages to forms.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 12, 15, 0, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 13,
                Title = "Optimize API Calls",
                Description = "Reduce API call overhead and improve performance.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 13, 10, 45, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 14,
                Title = "Add Toast Notifications",
                Description = "Show toast messages for success and errors.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 14, 11, 10, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 15,
                Title = "Write Unit Tests",
                Description = "Write basic unit tests for services.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 15, 9, 20, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 16,
                Title = "Setup GitHub Repository",
                Description = "Push code to GitHub and setup CI/CD.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 16, 14, 30, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 17,
                Title = "Configure Deployment",
                Description = "Configure deployment to test server.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 17, 16, 0, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 18,
                Title = "Review Code",
                Description = "Perform code review and refactoring.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 18, 11, 30, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 19,
                Title = "Update README",
                Description = "Document project setup and usage in README.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 19, 10, 10, 0, DateTimeKind.Utc),
                UserId = 1
            },
            new TaskItem
            {
                Id = 20,
                Title = "Prepare Project Demo",
                Description = "Prepare demo for project presentation.",
                Status = TaskStatus.Open,
                Priority = TaskPriority.Medium,
                DueDate = new DateTime(2024, 06, 25),
                CreatedAt = new DateTime(2024, 06, 20, 13, 0, 0, DateTimeKind.Utc),
                UserId = 1
            }

        );
    }

}
