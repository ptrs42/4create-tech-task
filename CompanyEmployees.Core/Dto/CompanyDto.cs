namespace CompanyEmployees.Core.Dto
{
    /// <summary>
    /// Class <c>CompanyDto</c> models the Company DTO
    /// </summary>
    public class CompanyDto
    {
        /// <summary>
        /// Property <c>CreatedAt</c> represents the time and date when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Property <c>Employees</c> represents the collection of employees assigned to the company.
        /// </summary>
        public EmployeeDto[]? Employees { get; set; }

        /// <summary>
        /// Property <c>Id</c> represents the unique Id of the company.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property <c>Name</c> represents the unique Name of the company.
        /// </summary>
        public string Name { get; set; } = null!;
    }
}