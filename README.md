# VroomParts

VroomParts is an ASP.NET Core MVC web application for browsing and managing automotive parts.

The project was built as an earlier learning and portfolio project focused on applying ASP.NET Core MVC, Entity Framework Core, SQL Server, authentication, layered application structure, and common e-commerce workflows.

## Features

### Customer

* Browse automotive parts
* Browse products by category
* View product details
* Shopping cart functionality
* Create and manage orders
* User registration and authentication
* Vehicle-based part recommendations

### Admin

* Manage automotive parts
* Manage product categories
* Manage vehicles
* Manage vehicle-to-part recommendations
* Review missing recommendation mappings
* Role-based access to administration features

## Tech Stack

* **C#**
* **ASP.NET Core MVC**
* **.NET 8**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Core Identity**
* **Razor Views**
* **Bootstrap**
* **HTML / CSS / JavaScript**

## Architecture

The project separates domain models, application logic and data access responsibilities.

```text
VroomParts/
├── Application/
│   ├── Products/
│   ├── Categories/
│   ├── Cart/
│   ├── Orders/
│   ├── Vehicles/
│   └── Recomendations/
│
├── Domain/
│   ├── Products/
│   ├── Categories/
│   ├── Cart/
│   ├── Orders/
│   ├── Car/
│   ├── Users/
│   └── VehicleRecommendations/
│
├── Data/
│   ├── Repository/
│   ├── Migrations/
│   └── ApplicationDBContext.cs
│
├── Areas/
│   ├── Admin/
│   ├── Customer/
│   └── Identity/
│
├── Views/
└── wwwroot/
```

The application uses repository and service abstractions to separate data access from application logic.

## Authentication & Authorization

Authentication is implemented using **ASP.NET Core Identity**.

The application contains separate customer and administrator functionality, with role-based authorization protecting administrative operations.

## Vehicle Part Recommendations

One of the additional features of the project is a recommendation system that associates automotive parts with compatible vehicles.

Administrators can manage vehicle-to-part mappings, while the customer-facing application can use these mappings when displaying relevant parts.

## Database

The application uses **SQL Server** with **Entity Framework Core**.

The repository includes EF Core migrations for creating and updating the database schema.

Example local connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=VroomParts;Trusted_Connection=True;TrustServerCertificate=True"
}
```

## Running Locally

### Prerequisites

* .NET 8 SDK
* SQL Server / SQL Server Express
* Entity Framework Core CLI tools

Clone the repository:

```bash
git clone https://github.com/MXR1XN/VroomParts_MVC.git
cd VroomParts_MVC/VroomParts
```

Restore dependencies:

```bash
dotnet restore
```

Update the database:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

Open the localhost URL displayed in the terminal.

## Project Status

This is an older portfolio/learning project.

It is kept on GitHub to demonstrate my earlier experience with ASP.NET Core MVC, relational databases, authentication, repository/service patterns and e-commerce application workflows.

My newer projects use a more API-oriented architecture and modern frontend technologies.

## What I Practiced

While building this project I worked with:

* MVC application structure
* Dependency injection
* Entity Framework Core
* Relational database modeling
* Repository pattern
* Application services
* Authentication and role-based authorization
* Shopping cart and order workflows
* Admin/customer separation using Areas
* Vehicle and product relationships
* Server-side rendering with Razor Views

## Author

**MXR1XN**

GitHub: https://github.com/MXR1XN
