using System.Data;
using System.Data.Entity;
using System.Web;
using Clinic.Data.DbContexts;

namespace Clinic.Service.Services.Transaction
{
    /// <summary>
    /// </summary>
    public class TransactionPerRequest : IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        #region Ctor

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="httpContext"></param>
        public TransactionPerRequest(IUnitOfWork unitOfWork, HttpContextBase httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        #endregion

        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpContextBase _httpContext;
        private const string Transaction = "_Transaction";
        private const string Error = "_Error";

        #endregion

        #region Interfaces

        /// <summary>
        /// </summary>
        void IRunOnEachRequest.Execute()
        {
            //_httpContext.Items[Transaction] = _unitOfWork.Database.BeginTransaction(IsolationLevel.Snapshot);
        }

        /// <summary>
        /// </summary>
        void IRunOnError.Execute()
        {
            _httpContext.Items[Error] = true;
        }

        /// <summary>
        /// </summary>
        void IRunAfterEachRequest.Execute()
        {
            var transaction = (DbContextTransaction) _httpContext.Items["_Transaction"];
            if (_httpContext.Items["_Error"] != null)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
        }

        #endregion
    }
}