namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>CannotCreateEmployeesWithSameTitleInCompanyError</c> represents an error which is
    /// returned if multiple employees with the same title should be created while creating a new company.
    /// </summary>
    public class CannotCreateEmployeesWithSameTitleInCompanyError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the
        /// <c>CannotCreateEmployeesWithSameTitleInCompanyError</c> class.
        /// </summary>
        public CannotCreateEmployeesWithSameTitleInCompanyError() :
            base(Constants.Errors.CannotCreateEmployeesWithSameTitleInCompanyErrorMessage, Constants.Keys.EmployeesTitleKey)
        {
        }
    }
}