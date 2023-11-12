namespace CompanyEmployees.Core.Dto
{
    /// <summary>
    /// Class <c>ResponseErrorDto</c> models the error response DTO
    /// </summary>
    public class ResponseErrorDto
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>ResponseErrorDto</c> class.
        /// </summary>
        /// <param name="key">The key of the parameter which causes the error.</param>
        /// <param name="message">The error message.</param>
        public ResponseErrorDto(string key, string message)
        {
            Key = key;
            Message = message;
        }

        /// <summary>
        /// The key of the parameter which causes the error.
        /// </summary>
        public string Key { get; init; }

        /// <summary>
        /// The error message.
        /// </summary>
        public string Message { get; init; }
    }
}