namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>CompanyDoesNotExistError</c> represents an error which is returned if a company
    /// with provided company Id does not exist while creating a new employee.
    /// </summary>
    public class CompanyDoesNotExistError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>CompanyDoesNotExistError</c> class.
        /// </summary>
        public CompanyDoesNotExistError() : base(Constants.Errors.CompanyDoesNotExistErrorMessage, Constants.Keys.CompanyIdsKey)
        {
        }
    }
}