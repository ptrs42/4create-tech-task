# Architecture

This document outlines the architecture and design decisions made during the implementation of this project such as which tools to use, which design patterns to employe and why.

## Solution structure

The top level directory holds the solution-wide configuration files, specification and documentation. The architectural pattern closely followed, although somewhat simplified for the purposes of this project, is the onion architecture. This works well with TDD approach which was followed up to a point.

Other architectural patterns considered were the hexagonal and clean architecture. Hexagonal architecture would work better with DDD approach which whould be a better choice if the project had more domain logic. Clean architecture is a good choice for seasoned engineers, but can be too verbose for more junior members of a team. This verbosity and potential need for quick onboarding of other members, while at the same time ensuring easy adherence to the pattern in the long run was the reason for not choosing clean architecture in this particular instance.

All publically exposed interfaces should be located one level above the concrete implementation of the interface if applicable, if not should be located in the Common project.

Following projects all exist:

### CompanyEmployees.Api

The ASP.NET Core Web API is the outermost layer. It encompasses the controllers and sets up dependency injection. Optionally, custom middleware can be placed here.

### CompanyEmployees.Common

The Common project, sometimes called the domain layer, is a place for all shared resources such as constants, errors, and domain models. Any utility classes and functions can be placed here.

### CompanyEmployees.Core

This is the application layer, a place for all the services that implement business logic and DTOs. 

DTOs define interfacing models between the business logic and the rest of the ecosystem.
This application implements a RESTful Web API, and by grouping DTOs with services in this project it would be fairly easy to make a new application implementing some other protocol (passing messages via a Message Queue / Broker, SOAP or another RPC protocol, etc.) or another application architecture entirely (microservices, serverless functions/lambdas, etc.) 

The Services encapsulate all business logic. This includes validation rules, ensuring data consistency, etc.

Some improvement points:

- Using a nuget package such as FluentValidation for implementing validators. Most of the validations in the current implementation rely on the state of the database so this was not implemented here. Such validators should validate the data based on certain domain rules which do not rely on the state of the database (e.g., use validators to enforce valid ranges, mutually exclusive options, etc. instead of checking the uniqueness of an email address or a company name.)
- Implement specification pattern for the purpose of running validations that do rely on the state of the database. This is a double edged sword, on one hand it keeps the code in the services small or at least smaller and the these validations become reusable. On the other hand if such a specification validation is used only once and there are many of them, this becomes much less readable.
- Use AutoMapper nuget package to add automatic mapping of entities to DTOs and vice versa. 

### CompanyEmployees.Persistence

This is the infrastructure layer. It contains persistence models, repositories and code first migrations based on EF Core as well as database contexts. This application uses two separate contexts. One is the ApplicationDbContext which is used for all strictly domain entities (Employee, Company). All these entities are auditable, meaning an audit log is created each time an entity is added or modified. The other context is the AuditDbContext. This one is used for auditing purposes and the SystemLog entity (which is essentially an audit log entry) is associated with it.

### CompanyEmployees.UnitTests

Tests for different parts of the application. Since TDD approach was used up to a point during the development of this project this was an integral part of the solution.

## Design principles

Following KISS principle along with good engineering practices from OOP and clean code paradigms is key. Simplicity, however should not be sacrificed. 

It is better to err on the side of verbosity and writin code which is more procedural in nature then to have too much complexity just to satisfy OOP and clean code practices. 

For example, sometimes it is better to have several classes that result in a type system that aids the engineers, but most of the time an enum will do just fine. Making a lot of validators might make sense if they can be reused, but doing manual validation might be a better approach when validation is done in pnly one place. Greater complexity should be an emergent property.  

## Dependency management

External libraries should be managed using the standard nuget package manager integrated in Visual Studio.

Dependency packages should be kept at minimal necessary. If a package is not needed anymore after refactoring it should be removed.

Projects should not be freely interdependent. A modeling project can be added and a dependency diagram generated to enforce the following rules:

- Api project should reference Common, Core, and Persistence projects. It should, however, only use services from the Core project in the Controllers and optionally some utilities from Common project. Everythig else may be used only while setting up dependency injection.
- Common project should not reference any other projects
- Core project should reference only Common and Persistence projects
- Persistence project may reference only the Common project
- UnitTests can reference all other projects under testing or needed for testing.

For a solution which has a greater number of projects or if there are projects shared between more solutions, an private package repository should be set up along with CI/CD pipelines for publishing new versions of such packages.

## Code style and quality

Solution-wide configurations enforcing coding style and best coding practices are already in place under ```.editorconfig``` and ```CodeMaid.config``` files. 

All projects are configured so that warnings are treated as errors. XML documentation is turned on so all missing documentation comments for public classes, methods, properties, enums, etc., will raise a warning which will also be treated as an error during compilation. 

Code metrics should be checked regularly. Maintainability index should be above 20 (indicator is green), Cyclomatic complexity should be below 25 for methods which is enforced by CA1502 maintanability rule which by default produces a warning if it reaches that number or above. Both Maintainability index and Cyclomatic complexity should be evaluated together.

Naming is very important and it is better to err on the side of verbosity.

## Data persistence and auditing

MySQL database was chosen in this specific case as it is easy to set up and almost all engineers, regardless of their seniority, are acustomed to it. For a real-world project, depending on the usecase, scalability and performance requirements, and other constraints, another database or cloud database service could be used.

In the Persistence project the interceptor pattern provided by EF Core is used to create and store audit logs. While this is a good practice/interview example, audit logs should rarely be made like this. Instead, using database built-in audit logs, or alternatively using stored procedures and triggers in the database would be a better approach. Depending on the use case, other approaches might be considered such as notifying an external service to create the audit log entry and thus splitting the responsibilities.

## User input validation

As stated before, user validation could be improved upon, although this particular example leaves little room for doing it in a meaningful way. For larger number of entities with more domain constraints that do not depend on the database state FluentValidation nuget package may be used.

## Error handling

Exception driven business logic should be discouraged. Avoid throwing exceptions. Instead return composable objects that can contain a valid result or an error (i.e., ```ResultOrError<TResult, TError>```). This approach, when implemented correctly, is typesafe, it is more maintainable and and less error prone prone then throwing and possibly catching exceptions somewhere along the call stack.

Combined with TDD and defensive coding practices will make the codebase less bug prone, although it may make the code more verbose in some cases.

## Logging

Logging was not tackled in this project at all. For a real-world project more attention should be paid to this, including the logging format, any log analytics, etc.

## Testing

The services for creating an employee and a company have tests written. This was done while implementing them, using TDD approach. Smaller pieces of code should be unit tested, but this implementation as it stands now leaves little room for that.

Visual Studio Enterprise has functionality to analyse code coverage and enforce the set rules. Other tools might be used instead such as dotCover.

## Other considerations for a production system

Other things should be considered for a production-grade solution and they are enumerated here.

### API Design

The API currently implemented is based on the specification document. In a real-world scenario this should be better designed.

### State Management

For apps with complex UI state management shoudl be defined.

### Monitoring and Health checks

Depending on how the application is deployed there are different options for monitoring the application and running health checks. Cloud-based solutions, especially using serverless code execution services like Azure Functions and AWS Lambdas would be preferable as no additional health cheks are needed beyond what is already integrated into the cloud provider system. A microservice architecture would probably use the sidecar pattern to implement these across different services comprising the system.

### Disaster recovery and backups

Backups and disaster recovery shoudl be a part of the architecture design of all production-grade systems. This is beyond the scope of this task.

### Scalability and performance

Constraints and requirements within particular use case should dictate how scalable / performat a system has to be. Ensuring complience can be achieved by running load tests.

### Complience with regulatory requirements

Certain domains, especially when taken in some particular regional context, may require that the implementation is compliant to some standard or regulation such as GDPR in the EU context and/or 21 CFR Part 11 and HIPAA un the US. These need to be enumerated, understood and automatic checks need to be put in place to enforce these in coding practices.

### Security

Authentication, Authorization and other socurity concerns are out of scope for this task, but should be considered carefully for any production-grade system.

### Localization and Internationalization

These should also be considered in a production-grade system but are out of scope for this project.
