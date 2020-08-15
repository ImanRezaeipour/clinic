namespace Clinic.FrameWork.Providers
{
    /// <summary>
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Store<T>(string key, T value) where T : class;
    }
}