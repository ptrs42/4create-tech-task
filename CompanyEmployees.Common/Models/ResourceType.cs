namespace CompanyEmployees.Common.Models
{
    /// <summary>
    /// Enum <c>ResourceType</c> enumerates all possible resource types in the system; used in SystemLog.
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// Skip this type
        /// </summary>
        Skip,

        /// <summary>
        /// An employee
        /// </summary>
        Employee,

        /// <summary>
        /// A company
        /// </summary>
        Company,
    }
}