namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>InternalServerError</c> represents an error which is returned if an Exception
    /// occurs while processing a request.
    /// </summary>
    public class InternalServerError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>InternalServerError</c> class.
        /// </summary>
        public InternalServerError() : base(Constants.Errors.InternalErrorMessage)
        {
        }
    }
}