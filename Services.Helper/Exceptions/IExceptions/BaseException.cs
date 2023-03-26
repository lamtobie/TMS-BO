namespace Services.Helper.Exceptions
{
    public class BaseException : Exception
    {
        public virtual string ErrorCode { get; set; } = "undefined";
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object ErrorData { get; set; } = null;

        public BaseException()
            : base()
        {
        }

        public BaseException(string message)
            : base(message)
        {
            ErrorMessages.Add(message);
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorMessages.Add(message);
        }

        public BaseException(string errorCode, string message): base(message)
        {
            ErrorCode = errorCode;
            ErrorMessages.Add(message);
        }

        public BaseException(string errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
            ErrorMessages.Add(message);
        }
    }
}
