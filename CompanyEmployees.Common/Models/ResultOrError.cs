using CompanyEmployees.Common.Errors;

namespace CompanyEmployees.Common.Models
{
    /// <summary>
    /// Class <c>ResultOrError</c> models a return type which can contain a result or an error and
    /// as such is similar to the Either monad in functional programming languages.
    /// </summary>
    /// <typeparam name="TResult">The result type</typeparam>
    /// <typeparam name="TError">
    /// The error type. Must implement the <c cref="IError">IError</c> interface.
    /// </typeparam>
    public class ResultOrError<TResult, TError> where TError : IError
    {
        private readonly TError? _error;

        private readonly TResult? _result;

        /// <summary>
        /// This constructor initializes an instance of the <c>ResultOrError</c> class with the result.
        /// </summary>
        /// <param name="result">The result instance</param>
        public ResultOrError(TResult result)
        {
            _result = result;
        }

        /// <summary>
        /// This constructor initializes an instance of the <c>ResultOrError</c> class with the error.
        /// </summary>
        /// <param name="error">The error instance</param>
        public ResultOrError(TError error)
        {
            _error = error;
        }

        /// <summary>
        /// This method gets the error
        /// </summary>
        /// <returns>The error.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the instance contains a result instead of an error.
        /// </exception>
        public TError GetError() => _error != null ? _error : throw new ArgumentNullException();

        /// <summary>
        /// This method gets the result
        /// </summary>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the instance contains an error instead of a result.
        /// </exception>
        public TResult GetResult() => _result != null ? _result : throw new ArgumentNullException();

        /// <summary>
        /// This method checks if the instance contains an error.
        /// </summary>
        /// <returns>True if the instance contains an error; otherwise, false.</returns>
        public bool HasError()
        {
            return _error != null;
        }

        /// <summary>
        /// Maps the error if it is of type <typeparamref name="TErrorCheck"/>
        /// </summary>
        /// <typeparam name="TErrorCheck">Type to check the error against</typeparam>
        /// <param name="map">
        /// The function that maps <typeparamref name="TError"/> to <typeparamref name="TResult"/>
        /// </param>
        /// <returns>
        /// A new instance of the <c>ResultOrError</c> class. If the current instance contains a
        /// result then returns the new instance with the new result type instance; otherwise,
        /// returns a new instance with the same error.
        /// </returns>
        public ResultOrError<TResult, TError> MapErrorType<TErrorCheck>(Func<TError, TResult> map) where TErrorCheck : TError
        {
            if (HasError() && _error is TErrorCheck)
            {
                return new(map(_error));
            }

            return this;
        }

        /// <summary>
        /// Maps the result to a new result type
        /// </summary>
        /// <typeparam name="TNewResult">New result type</typeparam>
        /// <param name="map">
        /// The function that maps <typeparamref name="TResult"/> to <typeparamref name="TNewResult"/>
        /// </param>
        /// <returns>
        /// A new instance of the <c>ResultOrError</c> class. If the current instance contains a
        /// result then returns the new instance with the new result type instance; otherwise,
        /// returns a new instance with the same error.
        /// </returns>
        public ResultOrError<TNewResult, TError> MapResult<TNewResult>(Func<TResult, TNewResult> map)
        {
            if (HasError())
            {
                return new ResultOrError<TNewResult, TError>(_error!);
            }

            return new(map(_result!));
        }
    }
}