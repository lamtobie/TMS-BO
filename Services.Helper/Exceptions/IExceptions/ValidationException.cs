namespace Services.Helper.Exceptions
{
    public class ValidationException : BaseException
    {
        public override string ErrorCode { get; set; } = "validate_undefined";

        public static void Requires(bool expected, string errorMessage)
        {
            if (!expected)
                throw new ValidationException(errorMessage);
        }

        public static void Requires(bool expected, string errorCode, string errorMessage)
        {
            if (!expected)
                throw new ValidationException(errorCode, errorMessage);
        }

        public ValidationException() : base()
        {
        }

        public ValidationException(string errorCode, string message) : base(errorCode, message)
        {

        }

        public ValidationException(string message) : base(message)
        {

        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
