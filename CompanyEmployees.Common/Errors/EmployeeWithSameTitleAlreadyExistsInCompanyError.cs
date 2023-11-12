namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>EmployeeWithSameTitleAlreadyExistsInCompanyError</c> represents an error which is
    /// returned if an employee with the same title exists in a company.
    /// </summary>
    public class EmployeeWithSameTitleAlreadyExistsInCompanyError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the
        /// <c>EmployeeWithSameTitleAlreadyExistsInCompanyError</c> class.
        /// </summary>
        public EmployeeWithSameTitleAlreadyExistsInCompanyError() :
            base(Constants.Errors.EmployeeWithSameTitleAlreadyExistsInCompanyErrorMessage, Constants.Keys.TitleKey)
        {
        }
    }
}