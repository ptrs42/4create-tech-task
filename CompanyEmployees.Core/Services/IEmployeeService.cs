using CompanyEmployees.Common.Errors;
using CompanyEmployees.Common.Models;
using CompanyEmployees.Core.Dto;

namespace CompanyEmployees.Core.Services
{
    /// <summary>
    /// Interface <see cref="IEmployeeService"/> can be used to create new companies
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Creates a new emplozee, making sure all business rules are applicable before.
        /// </summary>
        /// <param name="employee">The emplozee to be created.</param>
        /// <returns>
        /// A Task whose result contains <see cref="ResultOrError{TResult, TError}"/>. If
        /// successful, the result contained is a <see cref="EmployeeDto"/> representing the created
        /// employee; otherwise, contains an error implementing the <see cref="IError"/> interface
        /// </returns>
        Task<ResultOrError<EmployeeDto, IError>> CreateEmployee(CreateEmployeeDto employee);
    }
}