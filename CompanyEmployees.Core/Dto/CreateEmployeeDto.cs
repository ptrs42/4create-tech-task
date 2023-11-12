using CompanyEmployees.Common.Models;

namespace CompanyEmployees.Core.Dto
{
    /// <summary>
    /// Class <c>CreateEmployeeDto</c> models the Employee DTO used by the client to request
    /// creation of an employee entity.
    /// </summary>
    public class CreateEmployeeDto
    {
        /// <summary>
        /// Property <c>CompanyIds</c> represents the collection of company Ids to which the
        /// employee should be assigned.
        /// </summary>
        public int[]? CompanyIds { get; set; }

        /// <summary>
        /// Property <c>Email</c> represents the unique Email of the employee.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Property <c>Title</c> represents the title of the employee.
        /// </summary>
        public EmployeeTitle Title { get; set; }
    }
}