using System.Collections.Generic;
using System.Linq;
using Clinic.Core.Infrastructure.DependencyManagement;
using StructureMap;

namespace Clinic.Service.Managers.Events
{
    /// <summary>
    /// Event subscription service
    /// </summary>
    public class SubscriptionService : ISubscriptionService
    {
        #region Public Properties

        public static IContainer Container { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Get subscriptions
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Event consumers</returns>
        public IList<IEventHandler<T>> GetSubscriptions<T>()
        {
            //todo: handle with structuremap
            var instances = ContainerManager.Container.GetAllInstances<IEventHandler<T>>().ToList();
            return instances;
            //return EngineContext.Current.ResolveAll<IConsumer<T>>();
        }

        #endregion Public Methods
    }
}