# Karate School Management System - Assignment 3

This solution implements the Karate School Management System domain model from Assignment 2 using C# object-oriented programming and MSTest unit tests.

## Projects

- `KarateSchoolSystem` - class library containing domain classes.
- `KarateSchoolSystem.Tests` - MSTest unit test project.

## OOP Principles Demonstrated

- **Encapsulation:** properties are validated through constructors and controlled methods.
- **Inheritance:** `Student`, `Instructor`, and `Administrator` inherit from abstract base class `User`.
- **Polymorphism:** each main class overrides `ToString()` with role-specific output.
- **Abstraction:** interfaces include `IReportable`, `IPayable`, `IAttendanceTrackable`, and `IPaymentStrategy`.

## Design Patterns Used

1. **Factory Method Pattern** - `UserFactory` creates `Student`, `Instructor`, and `Administrator` objects.
2. **Strategy Pattern** - `IPaymentStrategy` supports interchangeable payment methods such as `CashPaymentStrategy` and `CardPaymentStrategy`.

## Run Tests

```bash
dotnet restore
dotnet test
```

## Collect Code Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

The `Coverage` folder includes a coverage report template and a coverage matrix mapping tests to assignment requirements. Run the command above locally or in Visual Studio to generate the official coverage artifact for Blackboard if required.
