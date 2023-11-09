using System.ComponentModel.DataAnnotations;
using CompanyEmployees.Common.Models;

namespace CompanyEmployees.Persistence.Entities
{
    public class Employee : AuditableEntity
    {
        public Employee(EmployeeTitle title, string email) : base()
        {
            Title = title;
            Email = email;
        }

        public Employee(int id, DateTime createdAt, EmployeeTitle title, string email) : base(id, createdAt)
        {
            Title = title;
            Email = email;
        }

        public IEnumerable<Company> Companies { get; init; } = new List<Company>();

        [Required]
        public string Email { get; set; }

        [Required]
        public EmployeeTitle Title { get; set; }
    }
}