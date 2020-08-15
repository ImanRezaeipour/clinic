namespace Clinic.Core.Types
{
    /// <summary>
    /// نشان دهنده انواع علمیاتی است که میتواند انجام شود
    /// </summary>
    public enum AuditType
    {
        /// <summary>
        /// درج رکود
        /// </summary>
        Create = 1,

        /// <summary>
        /// ویرایش
        /// </summary>
        Edit = 2,

        /// <summary>
        /// حذف فیزیکی
        /// </summary>
        Delete = 3,

        /// <summary>
        /// حذف نرم
        /// </summary>
        SoftDelete = 4,

        /// <summary>
        /// جستجو
        /// </summary>
        Search = 5,

        /// <summary>
        /// ثبت نام
        /// </summary>
        Register = 6,

        /// <summary>
        /// ویرایش
        /// </summary>
        EditMe = 7

    }
}