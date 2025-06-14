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

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "password", "supun" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Priority", "Status", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Setup project to code.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Prepare project structure", null, 1 },
                    { 2, new DateTime(2024, 6, 2, 9, 30, 0, 0, DateTimeKind.Utc), "Add business logic for Tasks.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Implement TaskService", null, 1 },
                    { 3, new DateTime(2024, 6, 3, 15, 45, 0, 0, DateTimeKind.Utc), "Add data seeding for Tasks and Users.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Seed initial data", null, 1 },
                    { 4, new DateTime(2024, 6, 4, 10, 15, 0, 0, DateTimeKind.Utc), "Implement simple username/password authentication with cookies.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Implement Authentication", null, 1 },
                    { 5, new DateTime(2024, 6, 5, 14, 0, 0, 0, DateTimeKind.Utc), "Develop login form with validation and SCSS styling.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Create Login Page UI", null, 1 },
                    { 6, new DateTime(2024, 6, 6, 16, 20, 0, 0, DateTimeKind.Utc), "Implement sorting of tasks by title.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Add task list sorting", null, 1 },
                    { 7, new DateTime(2024, 6, 7, 11, 5, 0, 0, DateTimeKind.Utc), "Implement search/filtering for task list.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Add task filtering", null, 1 },
                    { 8, new DateTime(2024, 6, 8, 13, 50, 0, 0, DateTimeKind.Utc), "Display task list and task form side by side.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Build Dashboard Page", null, 1 },
                    { 9, new DateTime(2024, 6, 9, 17, 40, 0, 0, DateTimeKind.Utc), "Make Dashboard responsive for mobile devices.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Style Dashboard for Mobile", null, 1 },
                    { 10, new DateTime(2024, 6, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Test all user flows and fix any bugs.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Test Full Application", null, 1 },
                    { 11, new DateTime(2024, 6, 11, 12, 30, 0, 0, DateTimeKind.Utc), "Allow user to logout and clear session.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Implement Logout Functionality", null, 1 },
                    { 12, new DateTime(2024, 6, 12, 15, 0, 0, 0, DateTimeKind.Utc), "Add validation messages to forms.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Improve Form Validation", null, 1 },
                    { 13, new DateTime(2024, 6, 13, 10, 45, 0, 0, DateTimeKind.Utc), "Reduce API call overhead and improve performance.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Optimize API Calls", null, 1 },
                    { 14, new DateTime(2024, 6, 14, 11, 10, 0, 0, DateTimeKind.Utc), "Show toast messages for success and errors.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Add Toast Notifications", null, 1 },
                    { 15, new DateTime(2024, 6, 15, 9, 20, 0, 0, DateTimeKind.Utc), "Write basic unit tests for services.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Write Unit Tests", null, 1 },
                    { 16, new DateTime(2024, 6, 16, 14, 30, 0, 0, DateTimeKind.Utc), "Push code to GitHub and setup CI/CD.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Setup GitHub Repository", null, 1 },
                    { 17, new DateTime(2024, 6, 17, 16, 0, 0, 0, DateTimeKind.Utc), "Configure deployment to test server.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Configure Deployment", null, 1 },
                    { 18, new DateTime(2024, 6, 18, 11, 30, 0, 0, DateTimeKind.Utc), "Perform code review and refactoring.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Review Code", null, 1 },
                    { 19, new DateTime(2024, 6, 19, 10, 10, 0, 0, DateTimeKind.Utc), "Document project setup and usage in README.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Update README", null, 1 },
                    { 20, new DateTime(2024, 6, 20, 13, 0, 0, 0, DateTimeKind.Utc), "Prepare demo for project presentation.", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Open", "Prepare Project Demo", null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
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
