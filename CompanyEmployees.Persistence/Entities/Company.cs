using System.ComponentModel.DataAnnotations;

namespace CompanyEmployees.Persistence.Entities
{
    /// <summary>
    /// Class <c>Company</c> models an auditable entity which represents a company in the system.
    /// </summary>
    public class Company : AuditableEntity
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>Company</c> class.
        /// </summary>
        public Company() : base()
        {
            Name = null!;
        }

        /// <summary>
        /// This constructor initializes a new instance of the <c>Company</c> class with a name.
        /// </summary>
        /// <param name="name">The name of the company.</param>
        public Company(string name) : base()
        {
            Name = name;
        }

        /// <summary>
        /// This constructor initializes a new instance of the <c>Company</c> class with an Id.
        /// </summary>
        /// <param name="id">The Id of the instance.</param>
        public Company(int id) : base(id)
        {
            Name = null!;
        }

        /// <summary>
        /// This constructor initializes a new instance of the <c>Company</c> class with an Id.
        /// </summary>
        /// <param name="id">The Id of the instance.</param>
        /// <param name="createdAt">The time and date when the entity was created.</param>
        /// <param name="name">The name of the company.</param>
        public Company(int id, DateTime createdAt, string name) : base(id, createdAt)
        {
            Name = name;
        }

        /// <summary>
        /// Property <c>Employees</c> represents a collection of <c cref="Employee">Employee</c>
        /// type which are the employees assigned to the instance of the company
        /// </summary>
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

        /// <summary>
        /// Property <c>Name</c> represents the unique Name of the company.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}