
# Customers Management API

This is a basic API application developed using Clean Architecture principles. The primary objective of this application is to maintain a centralized and secure customer database, with authenticated access to the data. The project aims to demonstrate the developer's ability to build applications using .NET Core and MySQL.

## Features

- Centralized Customer Database
- Secure access with authentication
- Best practices in software development


## Tools and Technologies

**FastEndpoints:** For defininig API endpoints

**FluentValidations:** For request validation

**MySQL:** As the Database

**Docker:** For containerization

**FluentAssertions:** For expressive unit test assertions

**Moq:** For mocking dependencies in tests

**xUnit:** For unit testing


## Demonstrated Techniques

- **Development of RESTful APIs:** Designing and implementing RESTful endpoints
- **Database Design:** Structuring the database schema for customer data
- **SQL:** Writing and executing SQL queries
- **Repository Pattern:** Abstracting database access logic
- **Clean Architecture:** Organizing code with separation of concerns
- **Domain-Driven Design (DDD):** Structuring code based on the domain model
- **Test-Driven Development (TDD):** Writing tests before code to drive development
- **Chain of Responsibility Pattern:** Handling a request by passing it through a chain of handlers
## Getting Started

# Prerequisites
- .NET 8
- MySQL
- Docker
## Project Structure

- **Api:** Contains API endpoints and request handling logic
- **Application:** Contains business logic and use cases
- **Domain:** Contains core entities and domain logic
- **Infrastructure:** Contains data access logic and external dependencies
- **Tests:** Contains unit and integration tests
