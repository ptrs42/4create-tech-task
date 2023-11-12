namespace CompanyEmployees.Common
{
    /// <summary>
    /// This static class contains all the constants needed for the project.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// This static class contains all error message constant strings.
        /// </summary>
        public static class Errors
        {
            /// <summary>
            /// Error message used when attempting to create employees with the same email.
            /// </summary>
            public const string CannotCreateEmployeesWithSameEmailErrorMessage = "Cannot create more than one employee in a company with the same email.";

            /// <summary>
            /// Error message used when attempting to create employees with the same title.
            /// </summary>
            public const string CannotCreateEmployeesWithSameTitleInCompanyErrorMessage = "Cannot create more than one employee in a company with the same title.";

            /// <summary>
            /// Error message used when attempting to create an employee while creaging a company
            /// and assigning it bot the email/title and an id.
            /// </summary>
            public const string CannotCreateEmployeeWithIdErrorMessage = "Cannot create an employee with an assigned Id. Properties Email and Title are mutually exclusive with the Id property.";

            /// <summary>
            /// Error message used when attempting to create an employee and assign it to a company
            /// that does not exist.
            /// </summary>
            public const string CompanyDoesNotExistErrorMessage = "The specified company does not exist.";

            /// <summary>
            /// Error message used when attempting to create a company with the same name
            /// </summary>
            public const string CompanyWithSameNameAlreadyExistsErrorMessage = "A company with the same name already exists.";

            /// <summary>
            /// Error message used when attempting to create a company and assign to it an employee
            /// that does not exist.
            /// </summary>
            public const string EmployeeDoesNotExistErrorMessage = "The specified employee does not exist.";

            /// <summary>
            /// Error message used when attempting to create an employee with the same email as
            /// another one already in the DB.
            /// </summary>
            public const string EmployeeWithSameEmailAlreadyExistsErrorMessage = "An Employee with the same email address already exists.";

            /// <summary>
            /// Error message used when attempting to create an employee with the same title as
            /// another one assigned to the same company already in the DB.
            /// </summary>
            public const string EmployeeWithSameTitleAlreadyExistsInCompanyErrorMessage = "An Employee with the same title already exists within the company.";

            /// <summary>
            /// Error message used when an Exception occurs while processing a request.
            /// </summary>
            public const string InternalErrorMessage = "An internal server error has occured.";

            /// <summary>
            /// Error message used when attempting to create an entity without setting its unique
            /// identifier property to a value (i.e., Name of the company or Email of an employee).
            /// </summary>
            public const string UniqueIdentifierCannotBeNullOrEmptyErrorMessage = "Unique identifier cannot be null or empty.";
        }

        /// <summary>
        /// This static class contains all keys on which errors can occur.
        /// </summary>
        public static class Keys
        {
            /// <summary>
            /// Key used when there is an error in companyIds key in the CreateEmployeeDto.
            /// </summary>
            public const string CompanyIdsKey = "companyIds";

            /// <summary>
            /// Key used when there is an error in email key in the CreateCompanyDto.
            /// </summary>
            public const string EmailKey = "email";

            /// <summary>
            /// Key used when there is an error in email key in the employees collection of a company.
            /// </summary>
            public const string EmployeesEmailKey = "employees.email";

            /// <summary>
            /// Key used when there is an error in Id key in the employees collection of a company.
            /// </summary>
            public const string EmployeesIdKey = "employees.id";

            /// <summary>
            /// Key used when there is an error in title key in the employees collection of a company.
            /// </summary>
            public const string EmployeesTitleKey = "employees.title";

            /// <summary>
            /// Key used when there is an error in name key in the CreateCompanyDto.
            /// </summary>
            public const string NameKey = "name";

            /// <summary>
            /// Key used when there is an error in title key in the CreateCompanyDto.
            /// </summary>
            public const string TitleKey = "title";
        }
    }
}