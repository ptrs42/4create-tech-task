namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>CompanyWithSameNameAlreadyExistsError</c> represents an error which is returned if
    /// a company with the same name already exists in the DB while creating a new company.
    /// </summary>
    public class CompanyWithSameNameAlreadyExistsError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the
        /// <c>CompanyWithSameNameAlreadyExistsError</c> class.
        /// </summary>
        public CompanyWithSameNameAlreadyExistsError() : base(Constants.Errors.CompanyWithSameNameAlreadyExistsErrorMessage, Constants.Keys.NameKey)
        {
        }
    }
}