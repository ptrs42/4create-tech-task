using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyEmployees.Common.Models;

namespace CompanyEmployees.Persistence.Entities
{
    /// <summary>
    /// Class <c>SystemLog</c> models a non-auditable entity which represents an audit log entry.
    /// </summary>
    public class SystemLog
    {
        /// <summary>
        /// This constructor initializes an instance of the <c>SystemLog</c> class.
        /// </summary>
        public SystemLog()
        {
        }

        /// <summary>
        /// This constructor initializes an instance of the <c>SystemLog</c> class with all the
        /// fields initialized
        /// </summary>
        /// <param name="resourceType">The resource type</param>
        /// <param name="createdAt">The date and time when the event happened</param>
        /// <param name="eventType">The event</param>
        /// <param name="changeset">The JSON representation of the changeset</param>
        /// <param name="comment">The description of the audit log entry</param>
        public SystemLog(ResourceType resourceType,
            DateTime createdAt,
            EventType eventType,
            string changeset,
            string comment)
        {
            ResourceType = resourceType;
            CreatedAt = createdAt;
            Event = eventType;
            Changeset = changeset;
            Comment = comment;
        }

        /// <summary>
        /// This constructor initializes an instance of the <c>SystemLog</c> class with all the
        /// fields initialized
        /// </summary>
        /// <param name="id">The unique id of the audit entry</param>
        /// <param name="resourceType">The resource type</param>
        /// <param name="createdAt">The date and time when the event happened</param>
        /// <param name="eventType">The event</param>
        /// <param name="changeset">The JSON representation of the changeset</param>
        /// <param name="comment">The description of the audit log entry</param>
        public SystemLog(int id,
            ResourceType resourceType,
            DateTime createdAt,
            EventType eventType,
            string changeset,
            string comment)
        {
            Id = id;
            ResourceType = resourceType;
            CreatedAt = createdAt;
            Event = eventType;
            Changeset = changeset;
            Comment = comment;
        }

        /// <summary>
        /// Property <c>Changeset</c> contains the JSON representation of the changeset.
        /// </summary>
        public string Changeset { get; set; } = string.Empty;

        /// <summary>
        /// Property <c>Comment</c> represents the description of the audit log entry.
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Property <c>CreatedAt</c> represents the time and date when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Property <c>EventType</c> represents the type of the event for which the audit log entry
        /// was created.
        /// </summary>
        public EventType Event { get; set; }

        /// <summary>
        /// Property <c>Id</c> represents the unique Id of the entity and is assigned by the
        /// database upon creation.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Property <c>ResourceType</c> represents the type of the resource (i.e., entity).
        /// </summary>
        public ResourceType ResourceType { get; set; }
    }
}