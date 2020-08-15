namespace Clinic.Service.Services.Permissions
{
    /// <summary>
    /// لیست تمام دسترسی های سیستم براساس اکشن های کنترلرها
    /// </summary>
    public static class PermissionConst
    {
        #region Public Fields

        public const string CanRoot = nameof(CanRoot);

        #region UserController

        public const string CanRootUser = nameof(CanRootUser);
        public const string CanUserEdit = nameof(CanUserEdit);
        public const string CanUserEasyRegister = nameof(CanUserEasyRegister);
        public const string CanUserDelete = nameof(CanUserDelete);
        public const string CanUserList = nameof(CanUserList);
        public const string CanUserMyEdit = nameof(CanUserMyEdit);

        #endregion UserController

        

        #endregion Public Fields
    }
}