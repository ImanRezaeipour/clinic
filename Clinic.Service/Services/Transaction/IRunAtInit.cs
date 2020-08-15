namespace Clinic.Service.Services.Transaction
{
    /// <summary>
    ///     اجرای وظایف در زمان بارگذاری اولیه‌ی برنامه
    /// </summary>
    public interface IRunAtInit
    {
        /// <summary>
        /// </summary>
        void Execute();
    }
}