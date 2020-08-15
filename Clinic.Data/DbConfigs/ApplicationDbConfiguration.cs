using System.Data.Entity;
using System.Data.Entity.SqlServer;
using Clinic.Data.Interceptors;

namespace Clinic.Data.DbConfigs
{
    /// <summary>
    /// </summary>
    public class ApplicationDbConfiguration : DbConfiguration
    {
        #region Public Constructors

        /// <summary>
        /// </summary>
        public ApplicationDbConfiguration()
        {
            SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
            AddInterceptor(new YeKePersianInterceptor());
        }

        #endregion Public Constructors
    }
}