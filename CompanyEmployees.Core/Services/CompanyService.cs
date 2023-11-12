using CompanyEmployees.Common;
using CompanyEmployees.Common.Errors;
using CompanyEmployees.Common.Models;
using CompanyEmployees.Core.Dto;
using CompanyEmployees.Persistence.Entities;
using CompanyEmployees.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace CompanyEmployees.Core.Services
{
    /// <summary>
    /// Class <c>CompanyService</c> is a concrete implementation of the <see
    /// cref="ICompanyService"/> interface.
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly ILogger _logger;

        /// <summary>
        /// This constructor initializes a new instance of the <c>CompanyService</c> class.
        /// </summary>
        /// <param name="companyRepository">The company repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="logger">The logger.</param>
        public CompanyService(IRepository<Company> companyRepository, IRepository<Employee> employeeRepository, ILogger logger)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ResultOrError<CompanyDto, IError>> CreateCompany(CreateCompanyDto company)
        {
            if (string.IsNullOrEmpty(company.Name))
            {
                return new(new UniqueIdentifierCannotBeNullOrEmptyError(Constants.Keys.NameKey));
            }

            if (company.Employees?.Any(e => e.Id == null && string.IsNullOrEmpty(e.Email)) ?? false)
            {
                return new(new UniqueIdentifierCannotBeNullOrEmptyError(Constants.Keys.EmployeesEmailKey));
            }

            if (company.Employees?.Any(e => e.Id != null && (!string.IsNullOrEmpty(e.Email) || e.Title != null)) ?? false)
            {
                return new(new CannotCreateEmployeeWithIdError());
            }

            using var transaction = await _companyRepository.BeginTransactionAsync();

            try
            {
                var companyIsUnique = await _companyRepository.AllAsync(c => c.Name != company.Name);

                if (!companyIsUnique)
                {
                    return new(new CompanyWithSameNameAlreadyExistsError());
                }

                var employeesToBeCreated = company.Employees?.Where(e => e.Email != null && e.Title != null) ?? Enumerable.Empty<EmployeeInCreateCompanyDto>();
                var areAllEmployeesUnique = await _employeeRepository.AllAsync(e => !employeesToBeCreated.Select(ec => ec.Email).Contains(e.Email));

                if (!areAllEmployeesUnique)
                {
                    return new(new EmployeeWithSameEmailAlreadyExistsError(Constants.Keys.EmployeesEmailKey));
                }

                var employeesWithIds = company.Employees?.Where(e => e.Id != null).Select(e => e.Id!.Value) ?? Enumerable.Empty<int>();
                var existingEmployees = _employeeRepository.WhereInclude(e => employeesWithIds.Contains(e.Id), e => e.Companies);
                var allEmployeesWithIdsExist = existingEmployees.Count() == employeesWithIds.Count();

                if (!allEmployeesWithIdsExist)
                {
                    return new(new EmployeeDoesNotExistError());
                }

                var allEmployeesToAddToCompany = existingEmployees.Concat(employeesToBeCreated.Select(e => new Employee(e.Title!.Value, e.Email!))).ToList();

                var hasDuplicateTitles = allEmployeesToAddToCompany.Count(e => e.Title == EmployeeTitle.Developer) > 1 ||
                    allEmployeesToAddToCompany.Count(e => e.Title == EmployeeTitle.Manager) > 1 ||
                    allEmployeesToAddToCompany.Count(e => e.Title == EmployeeTitle.Tester) > 1;

                if (hasDuplicateTitles)
                {
                    return new(new CannotCreateEmployeesWithSameTitleInCompanyError());
                }

                areAllEmployeesUnique = allEmployeesToAddToCompany.DistinctBy(e => e.Email).Count() == allEmployeesToAddToCompany.Count;

                if (!areAllEmployeesUnique)
                {
                    return new(new CannotCreateEmployeesWithSameEmailError());
                }

                var companyToCreate = new Company(company.Name)
                {
                    Employees = allEmployeesToAddToCompany
                };

                var createdCompany = await _companyRepository.AddAsync(companyToCreate);

                await transaction.CommitAsync();

                return new(new CompanyDto
                {
                    Id = createdCompany.Id,
                    CreatedAt = createdCompany.CreatedAt,
                    Name = createdCompany.Name,
                    Employees = createdCompany.Employees.Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        CreatedAt = e.CreatedAt,
                        Title = e.Title,
                        Email = e.Email,
                        CompanyIds = e.Companies.Select(c => c.Id).ToArray()
                    }).ToArray()
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