namespace Netflix.Application.Common.Errors
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; }

        public ValidationException(IEnumerable<ValidationError> errors)
        {
            this.Errors = errors;
        }

    }
}
