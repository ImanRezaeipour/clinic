using System.Data.Entity.Migrations;
using Clinic.Data.DbContexts;
using Clinic.Data.Migrations;

namespace Clinic.Data.DbConfigs
{
    /// <summary>
    /// </summary>
    public sealed class ApplicationDbMigrationsConfiguration : DbMigrationsConfiguration<BaseDbContext>
    {
        #region Public Constructors

        /// <summary>
        /// </summary>
        public ApplicationDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            SetSqlGenerator("System.Data.SqlClient", new ApplicationSqlServerMigrationSqlGenerator());
        }

        #endregion Public Constructors

        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(BaseDbContext context)
        {
        }

        #endregion Protected Methods
    }
}