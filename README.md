# FoodHackBD

## Project Overview
FoodHackBD is a web application to manage your food inventory, track food logs, and organize receipts or food label uploads.

## Tech Stack
- Backend: ASP.NET Core MVC, Entity Framework Core
- Frontend: Razor Pages, Bootstrap 5, HTML/CSS
- Database: SQL Server / LocalDB
- Authentication: ASP.NET Core Identity

## Setup Instructions
1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Update `appsettings.json` with your DB connection string.
4. Apply migrations: `dotnet ef database update`
5. Run the application.

## Seed Data
Add sample users, inventories, food logs, and resources if needed.

## Project Structure
- Controllers/
- Models/
- Views/
- wwwroot/
