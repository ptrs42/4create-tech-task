namespace CompanyEmployees.Common.Models
{
    /// <summary>
    /// Enum <c>EventType</c> enumerates all possible event types.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Skip this event (i.e., do not log)
        /// </summary>
        Skip,

        /// <summary>
        /// Create entity event
        /// </summary>
        Create,

        /// <summary>
        /// Updater entity event
        /// </summary>
        Update
    }
}