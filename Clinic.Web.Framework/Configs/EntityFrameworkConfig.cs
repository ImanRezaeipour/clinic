using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using Clinic.Data.DbContexts;
using ElmahEFLogger.CustomElmahLogger;

namespace Clinic.FrameWork.Configs
{
    /// <summary>
    /// </summary>
    public class EntityFrameworkConfig
    {
        /// <summary>
        /// </summary>
        public static void RegisterEntityFramework()
        {
            Database.SetInitializer<BaseDbContext>(null);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, DataLayer.Migrations.Configuration>());
            //ApplicationObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();

            // config audit when your application is starting up...
            //var auditConfiguration = AuditConfiguration.Default;
            //auditConfiguration.IncludeRelationships = false;
            //auditConfiguration.LoadRelationships = false;
            //auditConfiguration.DefaultAuditable = false;
            //AuditConfiguration.Default.IsAuditable<User>();
            //AuditConfiguration.Default.IsAuditable<Company>();
            //AuditConfiguration.Default.IsAuditable<Product>();

            //ad interception for logg EF errors
            DbInterception.Add(new ElmahEfInterceptor());
        }
    }
}