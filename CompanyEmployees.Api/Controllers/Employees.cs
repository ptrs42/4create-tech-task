using CompanyEmployees.Common;
using CompanyEmployees.Common.Errors;
using CompanyEmployees.Core;
using CompanyEmployees.Core.Dto;
using CompanyEmployees.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Api.Controllers
{
    /// <summary>
    /// Class <c cref="Employees">Employees</c> represents the API controller in charge of the <c
    /// cref="Employee">Employee</c> entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Employees : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// This constructor initializes a new instance of <c cref="Employees">Employees</c> controller.
        /// </summary>
        /// <param name="employeeService">
        /// <c>employeeService</c> is the instance of the <c
        /// cref="IEmployeeService">IEmployeeService</c> interface.
        /// </param>
        public Employees(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// This method is the API endpoint handler for creating an employee.
        /// </summary>
        /// <param name="employee">
        /// <c>employee</c> is the DTO representing the employee entity which should be created.
        /// </param>
        /// <returns>
        /// An instance of <c cref="IActionResult">IActionResult</c> with appropriate status code.
        /// In case of an error also returns the information about the key which caused an error and
        /// a message explaining the error.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto employee)
        {
            var result = await _employeeService.CreateEmployee(employee);

            return result.MapResult<IActionResult>(r => Ok(r))
                .MapErrorType<InternalServerError>(error => new StatusCodeResult(StatusCodes.Status500InternalServerError))
                .MapErrorType<IError>(error => Conflict(new ResponseErrorDto(error.GetKey(), error.GetMessage())))
                .GetResult();
        }
    }
}