namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>CannotCreateEmployeeWithIdError</c> represents an error which is returned if, while
    /// creating a company, user tries to create an employee whith bot Email/Title and Id assigned.
    /// </summary>
    public class CannotCreateEmployeeWithIdError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the
        /// <c>CannotCreateEmployeeWithIdError</c> class.
        /// </summary>
        public CannotCreateEmployeeWithIdError() : base(Constants.Errors.CannotCreateEmployeeWithIdErrorMessage, Constants.Keys.EmployeesIdKey)
        {
        }
    }
}