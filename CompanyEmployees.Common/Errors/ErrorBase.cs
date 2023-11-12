namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Abstract class <c>ErrorBase</c> models an error that contains an error message string.
    /// </summary>
    public abstract class ErrorBase : IError
    {
        private readonly string _key;
        private readonly string _message;

        /// <summary>
        /// This constructor initializes a new instance of the <c cref="ErrorBase">ErrorBase</c>
        /// class. Should be used by derived classes to load the error message string into the
        /// error. Sets the key to empty string.
        /// </summary>
        /// <param name="message"><c>message</c> is the error message string.</param>
        protected ErrorBase(string message)
        {
            _message = message;
            _key = string.Empty;
        }

        /// <summary>
        /// This constructor initializes a new instance of the <c cref="ErrorBase">ErrorBase</c>
        /// class. Should be used by derived classes to load the error message string and the key
        /// into the error.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        protected ErrorBase(string message, string key)
        {
            _message = message;
            _key = key;
        }

        /// <inheritdoc/>
        public string GetKey()
        {
            return _key;
        }

        /// <inheritdoc/>
        public string GetMessage()
        {
            return _message;
        }
    }
}