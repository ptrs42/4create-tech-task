# Development Environment Setup

To setup the development environment follow these steps in order.

1. Install Visual Studio 2022 and make sure to select ASP.NET and web development option.
1. Configure Visual studio to Place 'System' directives first when sorting usings
1. Configure Visual Studio to run cleanup on save
1. Install CodeMaid and import the configuration located in ```CodeMaid.config```
1. Install [MySQL server >=v8.2](https://dev.mysql.com/downloads/mysql/) and configure it using the MySQL Configurator
1. Optionally install [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)
1. Get the source code of the project using git
1. Make a copy of ```appsettings.Development-template.json``` in the CompanyEmployees.Web project and rename it to ```appsettings.Development.json```, then change connection string to match your MySQL server configuration.
1. In Visual Studio open the package manager console and run ```Update-Database -StartupProject CompanyEmployees.Web -Project CompanyEmployees.Persistence```
1. 
