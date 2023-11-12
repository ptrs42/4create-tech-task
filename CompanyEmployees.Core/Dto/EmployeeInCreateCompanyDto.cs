using CompanyEmployees.Common.Models;

namespace CompanyEmployees.Core.Dto
{
    /// <summary>
    /// Class <c>EmployeeInCreateCompanyDto</c> models the Employee DTO used by the client to
    /// request that the user is assigned to a company and optionally created as well.
    /// </summary>
    public class EmployeeInCreateCompanyDto
    {
        /// <summary>
        /// Property <c>Email</c> represents the optional unique Email of the employee, used only if
        /// a new employee should be created.
        /// </summary>
        public string? Email { get; set; } = null!;

        /// <summary>
        /// Property <c>Id</c> represents the optional unique Id of the employee, used only if an
        /// existing employee should be added to the company.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Property <c>Title</c> represents the optional title of the employee, used only if a new
        /// employee should be created.
        /// </summary>
        public EmployeeTitle? Title { get; set; }
    }
}