namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>EmployeeDoesNotExistError</c> represents an error which is returned if an employee
    /// with the provided Id does not exist when creating a new company.
    /// </summary>
    public class EmployeeDoesNotExistError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>EmployeeDoesNotExistError</c> class.
        /// </summary>
        public EmployeeDoesNotExistError() : base(Constants.Errors.EmployeeDoesNotExistErrorMessage, Constants.Keys.EmployeesIdKey)
        {
        }
    }
}