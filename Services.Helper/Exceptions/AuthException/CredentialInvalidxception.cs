using Exceptions;

namespace Services.Helper.Exceptions.AuthException
{
    public class CredentialInvalidException : ApiException
    {
        public CredentialInvalidException() : base()
        {
            ErrorCode = "CREDENTIAL_INVALID";
            ErrorMessages = new List<string>()
            {
                "Dữ liệu người dùng không chính xác"
            };
        }
    }
}