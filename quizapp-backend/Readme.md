# Quiz Application - ASP.NET Core Web API with Entity Framework Core, Microsoft SQL Server

## Description
This is a Quiz Application built with ASP.NET Core Web API, Entity Framework Core, and Microsoft SQL Server. It provides a platform for users to take quizzes and for administrators to manage quiz questions and answers.

## Features
- User authentication and authorization
- CRUD operations for quiz questions and answers
- Pagination and filtering of quiz questions
- API versioning
- API documentation with Swagger

## Technologies Used
- ASP.NET Core 5.0
- Entity Framework Core 5.0
- Microsoft SQL Server
- Swagger / OpenAPI

## Getting Started
### Prerequisites
- .NET 5.0 or later
- Microsoft SQL Server
- Docker (optional)

### Installation
1. Clone the repository
```bash
git clone https://github.com/abcd/QuizApp
```

2. Change directory
```bash
cd QuizApp
```

3. Update the connection string in `appsettings.json` to point to your SQL Server instance
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=QuizApp;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

4. Run the following commands to create the database and seed the initial data
```bash
dotnet ef database update
```

5. Run the application
```bash
dotnet watch --project QuizApp.WebAPI
```

6. Open your browser and navigate to `https://localhost:5195` to view the API documentation

## Branches
Check out the different branches to see the code for each task

- `main` - contains the latest stable code
- `asp/task-*` - contains the code for a specific task of asp.net core web api
- `ef/task-*` - contains the code for a specific task of entity framework core
