using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyEmployees.Persistence.Entities
{
    public abstract class AuditableEntity
    {
        public AuditableEntity()
        {
        }

        public AuditableEntity(int id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }

        public DateTime CreatedAt { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}