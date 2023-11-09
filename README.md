# CompanyEmployees

This is tech interview task implementation for 4Create.

## Specification

The original specification document is located under ```spec/C# .NET test.pdf```.

## Documentation

The documentation is located under the ```doc/``` directory.
This includes the Develpment Environment Setup document, Design and Architecture document.

## Solution

There are several projects within this solution, namely:

- CompanyEmployees.Web - The ASP.NET Core Web API, includes the DTOs
- CompanyEmployees.Core - Place for all the services and business logic
- CompanyEmployees.Shared - Place for all shared classes, etc.
- CompanyEmployees.Persistence - Contains persistence models, repositories and code first migrations based on EF Core
- CompanyEmployees.UnitTests - Unit tests for different parts of the application
