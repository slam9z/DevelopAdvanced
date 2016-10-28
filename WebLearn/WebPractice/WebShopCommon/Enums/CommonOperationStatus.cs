namespace WebShopCommon.Enums
{
    public enum CommonOperationStatus
    {
        InvalidTime = -7,

        InvalidUserNameOrPassword = -6,

        InvalidCaptcha = -5,

        Forbidden = -4,

        OverLimit = -3,

        NotFound = -2,

        AlreadyExist = -1,

        Default = 0,

        Success = 1
    }
}
