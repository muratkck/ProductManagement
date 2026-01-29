# Product Management API

ASP.NET Core Web API project - RESTful API for product management

## Technologies

- .NET 10.0
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Swagger/OpenAPI

## Setup

### Prerequisites

- .NET 10.0 SDK
- PostgreSQL 12+
- Visual Studio 2022 or VS Code

### Installation Steps

1. Clone the repository:
```bash
git clone <repository-url>
cd ProductManagement
```

2. Configure database connection using User Secrets:
```bash
cd ProductManagement
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=ProductManagementDb;Username=postgres;Password=YOUR_PASSWORD"
```

3. Run database migration:
```bash
dotnet ef database update
```

4. Run the application:
```bash
dotnet run
```

## API Endpoints

Swagger UI: `https://localhost:5001/swagger`

### Products (Desired endpoints)

- `GET /api/products` - List all products
- `GET /api/products/{id}` - Get product details
- `POST /api/products` - Create new product
- `DELETE /api/products/{id}` - Delete product

## Project Structure
```
ProductManagement/
├── Controllers/     # API endpoints
├── Services/        # Business logic
├── Repositories/    # Data access layer
├── Models/          # Entity models
├── DTOs/            # Data transfer objects
└── Data/            # DbContext
```

## Development

The project follows Layered Architecture principles:
- Controller → Service → Repository → Database
