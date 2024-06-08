
# Customers Management API

This is a basic API application developed using Clean Architecture principles. The primary objective of this application is to maintain a centralized and secure customer database, with authenticated access to the data. The project aims to demonstrate the developer's ability to build applications using .NET Core and MySQL.

## Features

- Centralized Customer Database
- Secure access with authentication
- Best practices in software development

## Use Cases

1) Given a Customer entity with the following properties: Name, Email, Phone
  - Where:
    - Name can be up to 100 chars and not be empty
    - Email must be a valid email address, it cannot be empty and can have a maximum length of 255 characters.
    - Phone cannot be empty, it must start with a '+' sign for the country code, and it must have between 12 and 14 digits after the sign. 
    - None of them can be null 
- Provide a CRUD API with the following endpoints
  - Create
  - Update
  - Delete
  - List
    - Where
      - The list must be split into 10, 20, or 50 records per page.
      - The data must be followed by the total number of records in the table so a proper pagination can be built on the UI.
  - Details
- All endpoints must require a JWTBearer authentication token to be executed

2) Given a User entity with the following properties: Name, Email, Password, Active, CreatedAt, UpdatedAt, DeactivatedAt
   - Where:
    - Name can be up to 100 chars and not be empty
    - Email must be a valid email address, it cannot be empty and can have a maximum length of 255 characters.
    - Password must have at least 6 characters in length.
    - Active cannot be modified by any endpoint. The default value is active.
    - CreatedAt will be set automatically with the creation time in UTC.
    - UpdatedAt will keep track of the user data's last modification.
    - DeactivatedAt will have a default value of null, and it's intended to be used on a later feature. It cannot be changed on this system.
    - None of the above can be null
  - Provide the following endpoints:
    - Login: Receives email and password and returns a JWT token when authentication succeeds that can be used to execute the customer's endpoints.
    - Details: To get non-sensitive data from a specific user.


## Tools and Technologies

**FastEndpoints:** For defininig API endpoints

**FluentValidations:** For request validation

**MySQL:** As the Database

**Docker:** For containerization

**FluentAssertions:** For expressive unit test assertions

**Moq and AutoMocker:** For mocking dependencies in tests

**xUnit:** For unit testing


## Demonstrated Techniques

- **SOLID Principles:** For OOP Development
- **Development of RESTful APIs:** Designing and implementing RESTful endpoints
- **Database Design:** Structuring the database schema for customer data
- **SQL:** Writing and executing SQL queries
- **Repository Pattern:** Abstracting database access logic
- **Clean Architecture:** Organizing code with separation of concerns
- **Domain-Driven Design (DDD):** Structuring code based on the domain model
- **Test-Driven Development (TDD):** Writing tests before code to drive development

## Getting Started

## Prerequisites
- .NET 8
- MySQL
- Docker
## Project Structure

- **Api:** Contains API endpoints and request-handling logic
- **Application:** Contains business logic and use cases
- **Domain:** Contains core entities and domain logic
- **Infrastructure:** Contains data access logic and external dependencies
- **Tests:** Contains unit and integration tests
