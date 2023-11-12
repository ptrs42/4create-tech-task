namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Interface <c>IError</c> defines the necessary interface for all errors
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// This method gets the key where the error was encountered.
        /// </summary>
        /// <returns>The key string.</returns>
        string GetKey();

        /// <summary>
        /// This method gets the error message string.
        /// </summary>
        /// <returns>The error message string.</returns>
        string GetMessage();
    }
}