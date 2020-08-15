using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Domains.Users;
using Clinic.Core.Types;
using Clinic.Core.Utilities.Http;
using Clinic.Data.Constants;
using Clinic.Data.Conventions;
using Clinic.Data.Mappings.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Clinic.Core.Helpers;
using EntityState = System.Data.Entity.EntityState;

namespace Clinic.Data.DbContexts
{
    /// <summary>
    /// </summary>
    public class BaseDbContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim>, IUnitOfWork
    {

        public BaseDbContext():base("ApplicationConnection")
        {
            
        }
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether auto detect changes setting is enabled (used in EF)
        /// </summary>
        public virtual bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool ForceNoTracking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether proxy creation setting is enabled (used in EF)
        /// </summary>
        public virtual bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool ValidateOnSaveEnabled
        {
            get
            {
                return Configuration.ValidateOnSaveEnabled;
            }
            set
            {
                Configuration.ValidateOnSaveEnabled = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            var dbset = Set<TEntity>().Add(entity);
            SaveAllChanges();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        public void AddThisRange<TEntity>(IEnumerable<TEntity> entities)  where TEntity : class
        {
            ((DbSet<TEntity>)Set<TEntity>()).AddRange(entities);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void BulkInsertData<T>(IEnumerable<T> data)
        {
            AutoDetectChangesEnabled = false;
            ValidateOnSaveEnabled = false;
            // base.BulkInsertData<T>(data);
        }

        /// <summary>
        /// Create database script
        /// </summary>
        /// <returns>SQL to generate database</returns>
        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        /// <summary>
        /// Detach an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
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
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            bool acd = this.Configuration.AutoDetectChangesEnabled;
            try
            {
                this.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                this.Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
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
        public void RejectChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <typeparam name="TEntity"></typeparam>
        ///  <param name="entity"></param>
        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
            SaveAllChanges();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IDbSet<TEntity> Repository<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="invalidateCacheDependencies"></param>
        /// <param name="auditUserId"></param>
        /// <returns></returns>
        public int SaveAllChanges(bool invalidateCacheDependencies = true, Guid? auditUserId = null)
        {
            UpdateValue(auditUserId);

            var result = SaveChanges();
            //if (result <= 0)
            //    throw new ServiceException("");

            return GetSaveChangeResult(invalidateCacheDependencies, result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="invalidateCacheDependencies"></param>
        /// <param name="auditUserId"></param>
        /// <returns></returns>
        public async Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true, Guid? auditUserId = null)
        {
            UpdateValue(auditUserId);

            var result = await SaveChangesAsync();
            //if (result <= 0)
            //    throw new ServiceException("");

            return GetSaveChangeResult(invalidateCacheDependencies, result);
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }

            //entity is already loaded
            return alreadyAttached;
        }

        /// <summary>
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Conventions.Add(new PluralizeConvention());

            modelBuilder.Configurations.AddFromAssembly(typeof(BaseConfig<>).Assembly);

            AddEntities(typeof(BaseEntity).Assembly, modelBuilder, UowConst.EntitiesNamespace);
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="assembly">         </param>
        /// <param name="modelBuilder"></param>
        /// <param name="nameSpace">   </param>
        private static void AddEntities(Assembly assembly, DbModelBuilder modelBuilder, string nameSpace)
        {
            var entityTypes = assembly.GetTypes()
                .Where(type => type.BaseType != null && type.Namespace == nameSpace && type.BaseType == null)
                .ToList();
            entityTypes.ForEach(modelBuilder.RegisterEntityType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="invalidateCacheDependencies"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static int GetSaveChangeResult(bool invalidateCacheDependencies, int result)
        {
            if (!invalidateCacheDependencies)
                return result;
            //var changedEntityNames = GetChangedEntityNames();
            //new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entry"></param>
        private static void UpdateFieldsForAdded(DbEntityEntry<BaseEntity> entry)
        {
            //var auditUserIp = HttpContext.Current.Request.GetIp();
            //var auditUserAgent = HttpContext.Current.Request.GetBrowser();
            var auditDate = DateTime.Now;
            if (entry.Entity.Id == Guid.Empty)
                entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
            // entry.Entity.CreatedOn = auditDate;
            // entry.Entity.ModifiedOn = auditDate;
            //entry.Entity.Audit = AuditType.Create;
            //entry.Entity.CreatorIp = auditUserIp;
            //entry.Entity.ModifierIp = auditUserIp;
            //entry.Entity.CreatorAgent = auditUserAgent;
            //entry.Entity.ModifierAgent = auditUserAgent;
            //entry.Entity.Version = 1;
        }

        /// <summary>
        ///B
        /// </summary>
        /// <param name="entry"></param>
        private static void UpdateFieldsForModified(DbEntityEntry<BaseEntity> entry)
        {
            //var auditUserIp = HttpContext.Current.Request.GetIp();
            //var auditUserAgent = HttpContext.Current.Request.GetBrowser();
            var auditDate = DateTime.Now;
            // entry.Entity.ModifiedOn = auditDate;
            //entry.Entity.ModifierIp = auditUserIp;
            //entry.Entity.ModifierAgent = auditUserAgent;
            //entry.Entity.Version = ++entry.Entity.Version;
            //entry.Entity.Audit = entry.Entity.IsDelete.GetValueOrDefault()
            //? AuditType.SoftDelete
            //: AuditType.Edit;
            //entry.Entity.Version = 1;
        }

        private string[] GetChangedEntityNames()
        {
            return ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }

        /// <summary>
        /// </summary>
        /// <param name="auditUserId"></param>
        private void UpdateAuditFields(Guid auditUserId)
        {
            var auditUserIp = HttpContext.Current.Request.GetIp();
            var auditUserAgent = HttpContext.Current.Request.GetBrowser();
            var auditDate = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.Id == Guid.Empty)
                            entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
                        //entry.Entity.CreatedById = auditUserId;
                        //entry.Entity.ModifiedById = auditUserId;
                        UpdateFieldsForAdded(entry);
                        break;

                    case EntityState.Modified:
                        //entry.Entity.ModifiedById = auditUserId;
                        UpdateFieldsForModified(entry);
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var entry in ChangeTracker.Entries<Role>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.Id == Guid.Empty)
                            entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
                        entry.Entity.CreatedOn = auditDate;
                        entry.Entity.CreatedById = auditUserId;
                        entry.Entity.ModifiedOn = auditDate;
                        entry.Entity.Audit = AuditType.Create;
                        // entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.CreatorIp = auditUserIp;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.CreatorAgent = auditUserAgent;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = 1;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = auditDate;
                        // entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = ++entry.Entity.Version;
                        entry.Entity.Audit = entry.Entity.IsDelete.GetValueOrDefault()
                            ? AuditType.SoftDelete
                            : AuditType.Edit;
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void UpdateEntityFields()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        UpdateFieldsForAdded(entry);

                        break;

                    case EntityState.Modified:
                        UpdateFieldsForModified(entry);
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="auditUserId"></param>
        private void UpdateValue(Guid? auditUserId)
        {
            if (auditUserId == null || auditUserId == Guid.Empty)
            {
                if (HttpContext.Current == null)
                {
                    UpdateEntityFields();
                    return;
                }
                if (HttpContext.Current.User == null)
                {
                    UpdateEntityFields();
                    return;
                }
                if (HttpContext.Current.User.Identity == null)
                {
                    UpdateEntityFields();
                    return;
                }
                var currentUserId = HttpContext.Current.User.Identity.IsAuthenticated ? Guid.Parse(HttpContext.Current.User.Identity.GetUserId()) : Guid.Empty;
                if (currentUserId == Guid.Empty)
                    UpdateEntityFields();
                else
                    UpdateAuditFields(currentUserId);
            }
            else
                UpdateAuditFields(auditUserId.Value);
        }

        #endregion Private Methods

        ///// <summary>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"> </param>
        ///// <param name="mapping"></param>
        ///// <returns></returns>
        //public T Update<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping) where T : class, new()
        //{
        //    return this.UpdateGraph(entity, mapping);
        //}
    }
}