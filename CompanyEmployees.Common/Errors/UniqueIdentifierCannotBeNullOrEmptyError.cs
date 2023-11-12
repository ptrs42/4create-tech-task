namespace CompanyEmployees.Common.Errors
{
    /// <summary>
    /// Class <c>UniqueIdentifierCannotBeNullOrEmptyError</c> represents an error which is returned
    /// if an entity creation is requested without a proper unique identifier value set.
    /// </summary>
    public class UniqueIdentifierCannotBeNullOrEmptyError : ErrorBase
    {
        /// <summary>
        /// This constructor initializes a new instance of the
        /// <c>UniqueIdentifierCannotBeNullOrEmptyError</c> class.
        /// </summary>
        /// <param name="key">The key</param>
        public UniqueIdentifierCannotBeNullOrEmptyError(string key) :
            base(Constants.Errors.UniqueIdentifierCannotBeNullOrEmptyErrorMessage, key)
        {
        }
    }
}