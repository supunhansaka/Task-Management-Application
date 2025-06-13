using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "IsCompleted", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Setup project to code.", false, "Prepare project structure", null },
                    { 2, new DateTime(2024, 6, 2, 9, 30, 0, 0, DateTimeKind.Utc), "Add business logic for Tasks.", false, "Implement TaskService", null },
                    { 3, new DateTime(2024, 6, 3, 15, 45, 0, 0, DateTimeKind.Utc), "Add data seeding for Tasks and Users.", true, "Seed initial data", null },
                    { 4, new DateTime(2024, 6, 4, 10, 15, 0, 0, DateTimeKind.Utc), "Implement simple username/password authentication with cookies.", true, "Implement Authentication", null },
                    { 5, new DateTime(2024, 6, 5, 14, 0, 0, 0, DateTimeKind.Utc), "Develop login form with validation and SCSS styling.", false, "Create Login Page UI", null },
                    { 6, new DateTime(2024, 6, 6, 16, 20, 0, 0, DateTimeKind.Utc), "Implement sorting of tasks by title.", false, "Add task list sorting", null },
                    { 7, new DateTime(2024, 6, 7, 11, 5, 0, 0, DateTimeKind.Utc), "Implement search/filtering for task list.", true, "Add task filtering", null },
                    { 8, new DateTime(2024, 6, 8, 13, 50, 0, 0, DateTimeKind.Utc), "Display task list and task form side by side.", false, "Build Dashboard Page", null },
                    { 9, new DateTime(2024, 6, 9, 17, 40, 0, 0, DateTimeKind.Utc), "Make Dashboard responsive for mobile devices.", false, "Style Dashboard for Mobile", null },
                    { 10, new DateTime(2024, 6, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Test all user flows and fix any bugs.", false, "Test Full Application", null },
                    { 11, new DateTime(2024, 6, 11, 12, 30, 0, 0, DateTimeKind.Utc), "Allow user to logout and clear session.", true, "Implement Logout Functionality", null },
                    { 12, new DateTime(2024, 6, 12, 15, 0, 0, 0, DateTimeKind.Utc), "Add validation messages to forms.", false, "Improve Form Validation", null },
                    { 13, new DateTime(2024, 6, 13, 10, 45, 0, 0, DateTimeKind.Utc), "Reduce API call overhead and improve performance.", false, "Optimize API Calls", null },
                    { 14, new DateTime(2024, 6, 14, 11, 10, 0, 0, DateTimeKind.Utc), "Show toast messages for success and errors.", true, "Add Toast Notifications", null },
                    { 15, new DateTime(2024, 6, 15, 9, 20, 0, 0, DateTimeKind.Utc), "Write basic unit tests for services.", false, "Write Unit Tests", null },
                    { 16, new DateTime(2024, 6, 16, 14, 30, 0, 0, DateTimeKind.Utc), "Push code to GitHub and setup CI/CD.", true, "Setup GitHub Repository", null },
                    { 17, new DateTime(2024, 6, 17, 16, 0, 0, 0, DateTimeKind.Utc), "Configure deployment to test server.", false, "Configure Deployment", null },
                    { 18, new DateTime(2024, 6, 18, 11, 30, 0, 0, DateTimeKind.Utc), "Perform code review and refactoring.", false, "Review Code", null },
                    { 19, new DateTime(2024, 6, 19, 10, 10, 0, 0, DateTimeKind.Utc), "Document project setup and usage in README.", true, "Update README", null },
                    { 20, new DateTime(2024, 6, 20, 13, 0, 0, 0, DateTimeKind.Utc), "Prepare demo for project presentation.", false, "Prepare Project Demo", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "password", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
