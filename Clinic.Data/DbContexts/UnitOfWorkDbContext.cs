using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RefactorThis.GraphDiff;

namespace Clinic.Data.DbContexts
{
    /// <summary>
    /// </summary>
    public class UnitOfWorkDbContext : BaseDbContext, IUnitOfWork
    {

        #region Public Constructors

        /// <summary>
        /// </summary>
        public UnitOfWorkDbContext() : base("ApplicationConnection")
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// </summary>
        bool IUnitOfWork.AutoDetectChangesEnabled
        {
            get { return Configuration.AutoDetectChangesEnabled; }
            set { Configuration.AutoDetectChangesEnabled = value; }
        }

        /// <summary>
        /// </summary>
        public bool ForceNoTracking { get; set; }

        /// <summary>
        /// </summary>
        public bool ProxyCreationEnabled
        {
            get { return Configuration.ProxyCreationEnabled; }
            set { Configuration.ProxyCreationEnabled = value; }
        }

        /// <summary>
        /// </summary>
        public bool ValidateOnSaveEnabled
        {
            get { return Configuration.ValidateOnSaveEnabled; }
            set { Configuration.ValidateOnSaveEnabled = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        public void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            ((DbSet<TEntity>)Set<TEntity>()).AddRange(entities);
        }

        public void Add<Tx>(Tx domain) where Tx : class
        {
            var dbset = Set<Tx>();
            dbset.Add(domain);
            SaveAllChangesAsync();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void BulkInsertData<T>(IEnumerable<T> data)
        {
            //this.BulkInsert(data);
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        public void DisableFiltering(string name)
        {
            //this.DisableFilter(name);
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        public void EnableFiltering(string name)
        {
            //this.EnableFilter(name);
        }

        /// <summary>
        /// </summary>
        /// <param name="name">     </param>
        /// <param name="parameter"></param>
        /// <param name="value">    </param>
        public void EnableFiltering(string name, string parameter, object value)
        {
            //this.EnableFilter(name).SetParameter(parameter, value);
        }

        /// <summary>
        /// </summary>
        public void ForceDatabaseInitialize()
        {
            Database.Initialize(true);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">       </param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"> </param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping) where T : class, new()
        {
            return this.UpdateGraph(entity, mapping);
        }

        #endregion Public Methods
    }
}