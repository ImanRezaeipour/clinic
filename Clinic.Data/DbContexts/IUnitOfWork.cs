using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Clinic.Core.Domains.Common;
using RefactorThis.GraphDiff;

namespace Clinic.Data.DbContexts
{
    /// <summary>
    /// اینترفیس مربوط به الگوی واحد کار
    /// </summary>
    public interface IUnitOfWork
    {
        #region Public Properties

        /// <summary>
        /// </summary>
        bool AutoDetectChangesEnabled { get; set; }

        /// <summary>
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// </summary>
        bool ForceNoTracking { get; set; }

        /// <summary>
        /// </summary>
        bool ProxyCreationEnabled { get; set; }

        /// <summary>
        /// </summary>
        bool ValidateOnSaveEnabled { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// برای درج لیستی از موجودیت ها استفاده میشود
        /// </summary>
        /// <typeparam name="TEntity"> نوع مدل </typeparam>
        /// <param name="entities"> لیستی از مدل مورد نظر </param>
        void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Add<Tx>(Tx domain) where Tx : class;
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        void BulkInsertData<T>(IEnumerable<T> data);

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        void DisableFiltering(string name);

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        void EnableFiltering(string name);

        /// <summary>
        /// </summary>
        /// <param name="name">     </param>
        /// <param name="parameter"></param>
        /// <param name="value">    </param>
        void EnableFiltering(string name, string parameter, object value);

        /// <summary>
        /// به منظور ایجاد سریع دیتابیس
        /// </summary>
        void ForceDatabaseInitialize();

        /// <summary>
        /// اجرای یک کوئری اس کیو ال
        /// </summary>
        /// <typeparam name="T"> نوع داده مورد نظر خروجی </typeparam>
        /// <param name="sql">       </param>
        /// <param name="parameters"> پارامتر های استفاده شده در کوئری </param>
        /// <returns></returns>
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;

        /// <summary>
        /// برای نشانه گذاری یک آبجکت که درج شده است
        /// </summary>
        /// <typeparam name="TEntity"> نوع موجودیت </typeparam>
        /// <param name="entity"> آبجکت ارسالی </param>
        void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// برای نشانه گذاری یک آبجکت که ویرایش شده است
        /// </summary>
        /// <typeparam name="TEntity"> نوع موجودیت </typeparam>
        /// <param name="entity"> آبجکت ارسالی </param>
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// برای نشانه گذاری یک آبجکت که حذف شده است
        /// </summary>
        /// <typeparam name="TEntity"> نوع موجودیت </typeparam>
        /// <param name="entity"> آبجکت ارسالی </param>
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// برای نشانه گذاری یک آبجکت که از کانتکس خارج شده است
        /// </summary>
        /// <typeparam name="TEntity"> نوع موجودیت </typeparam>
        /// <param name="entity"> آبجکت ارسالی </param>
        void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// ذخیره سازی با امکان مشخص کردن تکلیف داده های کش شده
        /// </summary>
        /// <param name="invalidateCacheDependencies"></param>
        /// <param name="auditUserId">                </param>
        /// <returns></returns>
        int SaveAllChanges(bool invalidateCacheDependencies = true, Guid? auditUserId = null);

        /// <summary>
        /// ذخیره سازی ناهمزمان با امکان مشخص کردن تکلیف داده های کش شده
        /// </summary>
        /// <param name="invalidateCacheDependencies"></param>
        /// <param name="auditUserId">                </param>
        /// <returns></returns>
        Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true, Guid? auditUserId = null);

        /// <summary>
        /// متد ذخیره سازی
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// متد ذخیره سازی به صورت ناهمزمان
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// متدی برای استفاده از الگوی مخزن توکار EF
        /// </summary>
        /// <typeparam name="TEntity"> نوع موجودیت </typeparam>
        /// <returns> IDbSet از موجودیت </returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        ///// <summary>
        ///// برای ویرایش ارتباط های چند به چند استفاده میشود
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"> </param>
        ///// <param name="mapping"></param>
        ///// <returns></returns>
        //T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping) where T : class, new();

        #endregion Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Remove<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new();

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// Detach an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Detach(object entity);

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        /// Create database script
        /// </summary>
        /// <returns>SQL to generate database</returns>
        string CreateDatabaseScript();
    }
}