namespace CompanyEmployees.Common.Models
{
    /// <summary>
    /// Class <c>SystemLogEntryChagesetItem</c> models a changeset item which contains the key, its
    /// old and new value.
    /// </summary>
    public class SystemLogEntryChagesetItem
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>SystemLogEntryChagesetItem</c>
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="newValue">The new value</param>
        /// <param name="oldValue">The old value</param>
        public SystemLogEntryChagesetItem(string key, string newValue, string oldValue)
        {
            Key = key;
            NewValue = newValue;
            OldValue = oldValue;
        }

        /// <summary>
        /// Property <c>Key</c> represents the property name.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Property <c>NewValue</c> represents the new value corresponding with the key.
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Property <c>OldValue</c> represents the old value corresponding with the key.
        /// </summary>
        public string OldValue { get; set; }
    }
}