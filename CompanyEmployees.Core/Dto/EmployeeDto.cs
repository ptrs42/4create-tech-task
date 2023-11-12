using CompanyEmployees.Common.Models;

namespace CompanyEmployees.Core.Dto
{
    /// <summary>
    /// Class <c>EmployeeDto</c> models the Employee DTO.
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Property <c>CompanyIds</c> represents the collection of company Ids to which the
        /// employee should be assigned.
        /// </summary>
        public int[] CompanyIds { get; set; } = null!;

        /// <summary>
        /// Property <c>CreatedAt</c> represents the time and date when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Property <c>Email</c> represents the unique Email of the employee.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Property <c>Id</c> represents the unique Id of the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property <c>Title</c> represents the title of the employee.
        /// </summary>
        public EmployeeTitle Title { get; set; }
    }
}