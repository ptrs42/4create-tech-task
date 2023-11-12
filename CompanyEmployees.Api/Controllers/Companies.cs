using CompanyEmployees.Common.Errors;
using CompanyEmployees.Core.Dto;
using CompanyEmployees.Core.Services;
using CompanyEmployees.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Api.Controllers
{
    /// <summary>
    /// Class <c cref="Companies">Companies</c> represents the API controller in charge of the <c
    /// cref="Company">Company</c> entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Companies : ControllerBase
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// This constructor initializes a new instance of <c cref="Companies">Companies</c> controller.
        /// </summary>
        /// <param name="companyService">
        /// <c>companyService</c> is the instance of the <c
        /// cref="ICompanyService">ICompanyService</c> interface.
        /// </param>
        public Companies(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// This method is the API endpoint handler for creating a company.
        /// </summary>
        /// <param name="company">
        /// <c>company</c> is the DTO representing the company entity which should be created.
        /// </param>
        /// <returns>
        /// An instance of <c cref="IActionResult">IActionResult</c> with appropriate status code.
        /// In case of an error also returns the information about the key which caused an error and
        /// a message explaining the error.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto company)
        {
            var result = await _companyService.CreateCompany(company);

            return result.MapResult<IActionResult>(r => Ok(r))
                .MapErrorType<InternalServerError>(error => new StatusCodeResult(StatusCodes.Status500InternalServerError))
                .MapErrorType<IError>(error => Conflict(new ResponseErrorDto(error.GetKey(), error.GetMessage())))
                .GetResult();
        }
    }
}