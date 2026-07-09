# PC Component Management API

A RESTful Web API developed with ASP.NET Core for managing PCs and their hardware components. The application supports PC records, individual components, component manufacturers, and component types while demonstrating backend development practices using Entity Framework Core, DTOs, service abstraction, and custom exception handling.

---

## Features

- Manage PC records and their associated hardware components
- Create and update PC configurations
- Retrieve detailed PC information
- Manage component manufacturers and component types
- DTO-based data transfer between application layers
- Entity Framework Core database integration
- Database migrations
- Service layer abstraction
- Custom exception handling

---

## Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- REST API

---

## Project Structure

```text
PCComponentManagementAPI
│
├── Controllers
│   └── PCsController.cs
│
├── Data
│   └── AppDbContext.cs
│
├── DTOs
│   ├── CreatePcDto.cs
│   ├── GetComponentDto.cs
│   ├── GetManufacturerDto.cs
│   ├── GetPcComponentDto.cs
│   ├── GetPcDetailsDto.cs
│   ├── GetPcDto.cs
│   ├── GetTypeDto.cs
│   └── UpdatePcDto.cs
│
├── Exceptions
│   └── NotFoundException.cs
│
├── Migrations
│
├── Models
│   ├── Component.cs
│   ├── ComponentManufacturer.cs
│   ├── ComponentType.cs
│   ├── PC.cs
│   └── PCComponent.cs
│
├── Services
│   ├── DbService.cs
│   └── IDbService.cs
│
└── Program.cs
```

---

## Domain Model

The application is built around the following main entities:

- **PC** — represents a computer configuration.
- **Component** — represents an individual hardware component.
- **PCComponent** — connects PCs with their associated components.
- **ComponentManufacturer** — represents the manufacturer of a hardware component.
- **ComponentType** — categorizes components by type.

---

## Architecture

The project separates responsibilities across several layers:

- **Controllers** handle incoming HTTP requests.
- **DTOs** define the data exchanged through the API.
- **Services** contain application and database access logic.
- **Models** represent the application's domain entities.
- **AppDbContext** manages database access through Entity Framework Core.
- **Custom Exceptions** provide structured handling of missing resources.

---

## Getting Started

### Clone the repository

```bash
git clone https://github.com/senaelifyorucu/pc-component-management-api.git
```

### Navigate to the project

```bash
cd pc-component-management-api
```

### Restore dependencies

```bash
dotnet restore
```

### Apply database migrations

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

---

## Learning Outcomes

Through this project, I strengthened my knowledge of:

- ASP.NET Core Web API development
- RESTful API design
- Entity Framework Core
- Relational database modeling
- DTO pattern
- Service layer abstraction
- Dependency injection
- LINQ
- Database migrations
- Custom exception handling
- Object-Oriented Programming

---

## Future Improvements

- JWT authentication and authorization
- Global exception-handling middleware
- Unit and integration testing
- Pagination and filtering
- Docker support
- Extended Swagger/OpenAPI documentation

---

## Author

**Sena Elif Yorucu**

Computer Science Student  
Polish-Japanese Academy of Information Technology (PJATK)
