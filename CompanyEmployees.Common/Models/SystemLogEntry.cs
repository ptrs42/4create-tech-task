namespace CompanyEmployees.Common.Models
{
    /// <summary>
    /// Class <c>SystemLogEntry</c> models an audit entry
    /// </summary>
    public class SystemLogEntry
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>SystemLogEntry</c> class.
        /// </summary>
        /// <param name="uniqueIdentifierValue">The unique identifier value</param>
        /// <param name="resourceType">The resource type</param>
        /// <param name="eventType">The event type</param>
        /// <param name="createdAt">When the entity under audit was created</param>
        /// <param name="comment">The audit comment</param>
        public SystemLogEntry(string uniqueIdentifierValue, ResourceType resourceType, EventType eventType, DateTime createdAt, string comment)
        {
            UniqueIdentifierValue = uniqueIdentifierValue;
            Event = eventType;
            ResourceType = resourceType;
            CreatedAt = createdAt;
            Comment = comment;
        }

        /// <summary>
        /// Property <c>Changeset</c> contains the JSON representation of the changeset.
        /// </summary>
        public List<SystemLogEntryChagesetItem> Changeset { get; set; } = new List<SystemLogEntryChagesetItem>();

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
        /// Property <c>ResourceType</c> represents the type of the resource (i.e., entity).
        /// </summary>
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// The value of the unique identifier
        /// </summary>
        public string UniqueIdentifierValue { get; set; }
    }
}