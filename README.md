# TwoDo
TwoDo is a task management API built with ASP.NET Core. It allows users to create, manage, and track their tasks. The API provides endpoints for task operations and user authentication.

Getting Started
To use this API, follow the instructions below.

Prerequisites
  .NET Core SDK (version 5.0 or later)
  PostgreSQL

Installation
Clone the repository:

    git clone https://github.com/your-username/TwoDo.git
    
Navigate to the project directory:

    cd TwoDo

    
Restore the NuGet packages:

    dotnet restore
    
Update the database connection string in the appsettings.json file:

    "ConnectionStrings": {
      "DefaultConnection": "YOUR_DATABASE_CONNECTION_STRING"
    }

    
Apply the database migrations:

    dotnet ef database update

    
Run the application:

    dotnet run
    
# User API
The User API allows users to manage user-related operations, such as creating users, retrieving user details, updating user information, and deleting users.

  User Endpoints

  - GET /api/users/admin: Retrieves all users. (Requires admin access)
  - GET /api/users/users: Retrieves user details for the authenticated user.
  - GET /api/users/admin/{id}: Retrieves user details by user ID. (Requires admin access)
  - POST /api/users/register: Registers a new user.
  - DELETE /api/users: Deletes the authenticated user.
  - DELETE /api/users/admin/{id}: Deletes a user by user ID. (Requires admin access)
  - PUT /api/users: Updates user information for the authenticated user.
  - PUT /api/users/admin/{id}: Updates user information by user ID. (Requires admin access)
  
# Assignment API
The Assignment API allows users to manage task-related operations, such as creating tasks, retrieving tasks, updating task information, and deleting tasks.

  Assignment Endpoints

  - GET /api/assignments/admin: Retrieves all tasks. (Requires admin access)
  - GET /api/assignments: Retrieves tasks for the authenticated user.
  - GET /api/assignments/admin/{id}: Retrieves a task by task ID. (Requires admin access)
  - POST /api/assignments: Creates a new task for the authenticated user.
  - POST /api/assignments/admin/{id}: Creates a new task for a specific user. (Requires admin access)
  - DELETE /api/assignments: Deletes a task for the authenticated user.
  - DELETE /api/assignments/admin/{id}: Deletes a task by task ID. (Requires admin access)
  - PUT /api/assignments: Updates a task for the authenticated user.
  - PUT /api/assignments/admin/{id}: Updates a task by task ID. (Requires admin access)
  - PUT /api/assignments/done: Toggles the status of a task as completed or not for the authenticated user.

# Authentication API
The Authentication controller handles the authentication process in the TwoDo API. It provides an endpoint for user login, which generates a JSON Web Token (JWT) for authenticated users.

Authentication Endpoint
  User Login
  - URL: /api/authentication/login
  - Method: POST
  - Description: Authenticates a user and generates a JWT.
  - Request Body:
    - UserDTO object (JSON) with email and password
  - Successful Response:
    - Code: 200 (OK)
    - Content: JWT string
  - Error Responses:
    - Code: 400 (Bad Request)
    - Content: "Wrong email"
    - Code: 400 (Bad Request)
    - Content: "Wrong password"
    
# Technologies Used

  - ASP.NET Core
  - Microsoft.EntityFrameworkCore
  - Microsoft.AspNetCore.Authorization
  - Microsoft.AspNetCore.Mvc
  - Microsoft.IdentityModel.Tokens
  - CORS
  - JWT (JSON Web Tokens)
  - PostgerSQL
