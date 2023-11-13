using CompanyEmployees.Common;
using CompanyEmployees.Common.Models;
using CompanyEmployees.Core.Dto;

namespace CompanyEmployees.Core
{
    /// <summary>
    /// Interface <see cref="ICompanyService"/> can be used to create new companies
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Creates a new company, making sure all business rules are applicable before.
        /// </summary>
        /// <param name="company">The company to be created.</param>
        /// <returns>
        /// A Task whose result contains <see cref="ResultOrError{TResult, TError}"/>. If
        /// successful, the result contained is a <see cref="CompanyDto"/> representing the created
        /// company; otherwise, contains an error implementing the <see cref="IError"/> interface
        /// </returns>
        Task<ResultOrError<CompanyDto, IError>> CreateCompany(CreateCompanyDto company);
    }
}