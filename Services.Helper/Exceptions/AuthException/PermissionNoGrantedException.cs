
namespace Services.Helper.Exceptions.AuthException
{
    public class PermissionNoGrantedException : ApiException
    {
        public PermissionNoGrantedException() : base()
        {
            ErrorCode = "PERMISSION_NO_GRANTED";
            ErrorMessages = new List<string>()
            {
                "Chưa cấp quyền truy cập"
            };
        }
    }
}