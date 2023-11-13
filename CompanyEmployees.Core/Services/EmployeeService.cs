using CompanyEmployees.Common;
using CompanyEmployees.Common.Errors;
using CompanyEmployees.Common.Models;
using CompanyEmployees.Core.Dto;
using CompanyEmployees.Persistence;
using CompanyEmployees.Persistence.Entities;
using Microsoft.Extensions.Logging;

namespace CompanyEmployees.Core.Services
{
    /// <summary>
    /// Class <c>EmployeeService</c> is a concrete implementation of the <see
    /// cref="IEmployeeService"/> interface.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        /// <summary>
        /// This constructor initializes a new instance of the <c>EmployeeService</c> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="companyRepository">The company repository.</param>
        /// <param name="logger">The logger.</param>
        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<Company> companyRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ResultOrError<EmployeeDto, IError>> CreateEmployee(CreateEmployeeDto employee)
        {
            if (string.IsNullOrEmpty(employee.Email))
            {
                return new(new UniqueIdentifierCannotBeNullOrEmptyError(Constants.Keys.EmailKey));
            }

            try
            {
                var isUnique = await _employeeRepository.AllAsync(e => e.Email != employee.Email);

                if (!isUnique)
                {
                    return new(new EmployeeWithSameEmailAlreadyExistsError(Constants.Keys.EmailKey));
                }

                var companies = _companyRepository.WhereInclude(c => employee.CompanyIds?.Contains(c.Id) ?? false,
                    c => c.Employees).ToList() ??
                    Enumerable.Empty<Company>();

                var allCompaniesExist = companies.Count() == (employee.CompanyIds?.Length ?? 0);

                if (!allCompaniesExist)
                {
                    return new(new CompanyDoesNotExistError());
                }

                var employeeTitleIsUniqueInAllCompanies = companies.SelectMany(c => c.Employees).All(e => e.Title != employee.Title);

                if (!employeeTitleIsUniqueInAllCompanies)
                {
                    return new(new EmployeeWithSameTitleAlreadyExistsInCompanyError());
                }

                var newEmployee = new Employee(employee.Title, employee.Email)
                {
                    Companies = companies
                };

                var createdEmployee = await _employeeRepository.AddAsync(newEmployee);

                return new(new EmployeeDto
                {
                    Id = createdEmployee.Id,
                    Email = createdEmployee.Email,
                    Title = createdEmployee.Title,
                    CreatedAt = createdEmployee.CreatedAt,
                    CompanyIds = createdEmployee.Companies.Select(c => c.Id).ToArray()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal Server Error occured.");
                return new(new InternalServerError());
            }
        }
    }
}