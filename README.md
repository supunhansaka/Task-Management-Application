# Task-Management-Application
 
# 📝 Task Management Application

A full-stack Task Management System built with:

- **ASP.NET Core (.NET 8) Web API** — Backend
- **Angular 20 (Standalone)** — Frontend
- **SQL Server** — Database
- **Cookie-based authentication** — Simple username/password login

---

## 📁 Project Structure

Task-Management-Application/
├── TaskManagement.API/ → .NET 8 Web API project
├── task-management-frontend/ → Angular 20 frontend

---

## ⚙️ Prerequisites

Ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js (v18+)](https://nodejs.org)
- [Angular CLI](https://angular.io/cli)
- [SQL Server Express or Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)

---

## 🚀 Getting Started

### ✅ 1. Clone the repository

1. git clone <your-repo-url>
2. cd Task-Management-Application

### ✅ 2. Run the Backend (.NET Web API)

Using Visual Studio

1. Open TaskManagement.API/TaskManagement.API.csproj in Visual Studio.

2. Set TaskManagement.API as the startup project.

3. Check appsettings.json /  and update the connection string if needed.

4. Open Package Manager Console:
   - Update-Database

### ✅ 3. Run the Frontend (Angular 20)

1. cd task-management-frontend
2. npm install
3. ng serve    


## 👤 Default Login Credentials

Use this account to log in:

  - Username: supun
  - Password: password

✅ These are seeded into the DB on first migration.


## 🔒 Authentication
This app uses cookie-based authentication (HttpContext.SignInAsync):

No JWT tokens used (per assignment instruction)

Session persists until logout or cookie expiry