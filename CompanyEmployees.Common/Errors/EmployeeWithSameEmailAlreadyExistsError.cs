namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>EmployeeWithSameEmailAlreadyExistsError</c> represents an error which is returned
    /// if an employee with the same email already esists in the DB.
    /// </summary>
    public class EmployeeWithSameEmailAlreadyExistsError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the
        /// <c>EmployeeWithSameEmailAlreadyExistsError</c> class.
        /// </summary>
        /// <param name="key">The key</param>
        public EmployeeWithSameEmailAlreadyExistsError(string key) : base(Constants.Errors.EmployeeWithSameEmailAlreadyExistsErrorMessage, key)
        {
        }
    }
}