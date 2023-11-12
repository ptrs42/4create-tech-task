namespace CompanyEmployees.Core.Dto
{
    /// <summary>
    /// Class <c>CreateCompanyDto</c> models the Company DTO used by the client to request creation
    /// of a company entity.
    /// </summary>
    public class CreateCompanyDto
    {
        /// <summary>
        /// Property <c>Employees</c> represents the collection of employees assigned to the company.
        /// </summary>
        public EmployeeInCreateCompanyDto[]? Employees { get; set; }

        /// <summary>
        /// Property <c>Name</c> represents the unique Name of the company.
        /// </summary>
        public string Name { get; set; } = null!;
    }
}