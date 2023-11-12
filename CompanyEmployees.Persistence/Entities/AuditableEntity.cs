using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyEmployees.Persistence.Entities
{
    /// <summary>
    /// Abstract class <c>AuditableEntity</c> models an entity which has an <c>Id</c> and
    /// <c>CreatedAt</c> properties and is subject to automatic audits.
    /// </summary>
    public abstract class AuditableEntity
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>AuditableEntity</c> class.
        /// </summary>
        public AuditableEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// This constructor initializes a new instance of the <c>AuditableEntity</c> class with an Id.
        /// </summary>
        /// <param name="id">The Id of the instance.</param>
        public AuditableEntity(int id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// This constructor initializes a new instance of the <c>AuditableEntity</c> class with an Id.
        /// </summary>
        /// <param name="id">The Id of the instance.</param>
        /// <param name="createdAt">The time and date when the entity was created.</param>
        public AuditableEntity(int id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Property <c>CreatedAt</c> represents the time and date when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Property <c>Id</c> represents the unique Id of the entity and is assigned by the
        /// database upon creation.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}