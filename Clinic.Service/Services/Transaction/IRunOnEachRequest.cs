namespace Clinic.Service.Services.Transaction
{
    /// <summary>
    ///     اجرای وظایف در ابتدای هر درخواست
    /// </summary>
    public interface IRunOnEachRequest
    {
        /// <summary>
        /// </summary>
        void Execute();
    }
}