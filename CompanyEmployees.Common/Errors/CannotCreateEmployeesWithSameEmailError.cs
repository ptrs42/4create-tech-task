namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>CannotCreateEmployeesWithSameEmailError</c> represents an error which is returned
    /// if multiple employees with the same email should be created while creating a new company.
    /// </summary>
    public class CannotCreateEmployeesWithSameEmailError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the
        /// <c>CannotCreateEmployeesWithSameEmailError</c> class.
        /// </summary>
        public CannotCreateEmployeesWithSameEmailError() :
            base(Constants.Errors.CannotCreateEmployeesWithSameEmailErrorMessage, Constants.Keys.EmployeesEmailKey)
        {
        }
    }
}