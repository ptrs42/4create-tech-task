using System.ComponentModel.DataAnnotations;

namespace CompanyEmployees.Persistence.Entities
{
    public class Company : AuditableEntity
    {
        public Company(string name) : base()
        {
            Name = name;
        }

        public Company(int id, DateTime createdAt, string name) : base(id, createdAt)
        {
            Name = name;
        }

        public IEnumerable<Employee> Employees { get; init; } = new List<Employee>();

        [Required]
        public string Name { get; set; }
    }
}