using System.ComponentModel.DataAnnotations;
using CompanyEmployees.Common.Models;

namespace CompanyEmployees.Persistence.Entities
{
    /// <summary>
    /// Class <c>Employee</c> models an auditable entity which represents an employee in the system.
    /// </summary>
    public class Employee : AuditableEntity
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>Employee</c> class with a title
        /// and an email.
        /// </summary>
        /// <param name="title">The title of the employee.</param>
        /// <param name="email">The unique email of the employee.</param>
        public Employee(EmployeeTitle title, string email) : base()
        {
            Title = title;
            Email = email;
        }

        /// <summary>
        /// This constructor initializes a new instance of the <c>Employee</c> class with a title
        /// and an email along with the Id and CreatedAt properties.
        /// </summary>
        /// <param name="id">The unique Id.</param>
        /// <param name="createdAt">The time and date when the entity was created.</param>
        /// <param name="title">The title of the employee.</param>
        /// <param name="email">The unique email of the employee.</param>
        public Employee(int id, DateTime createdAt, EmployeeTitle title, string email) : base(id, createdAt)
        {
            Title = title;
            Email = email;
        }

        /// <summary>
        /// Property <c>Companies</c> represents a collection of <c cref="Company">Company</c> type
        /// which are the companies to witch the instance of the employee is assigned to.
        /// </summary>
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();

        /// <summary>
        /// Property <c>Email</c> represents the unique Email of the Employee.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Property <c>Title</c> represents the Title of the Employee. Each company can have no
        /// more than one employee witht the same title.
        /// </summary>
        [Required]
        public EmployeeTitle Title { get; set; }
    }
}