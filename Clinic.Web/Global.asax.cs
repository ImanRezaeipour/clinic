using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Clinic.Core.Infrastructure.DependencyManagement;
using Clinic.FrameWork.Configs;
using Clinic.FrameWork.StructureMap;
using Clinic.Service.Services.Transaction;
using Clinic.Web.Framework.Configs;
using StructureMap.Web.Pipeline;

namespace Clinic.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            foreach (var task in ContainerManager.Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            try
            {
                foreach (var task in ContainerManager.Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            catch (Exception)
            {
                HttpContextLifecycle.DisposeAndClearAll();
            }
        }

        /// <summary>
        /// </summary>
        protected void Application_Error()
        {
            foreach (var task in ContainerManager.Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        /// <summary>
        /// </summary>
        protected void Application_Start()
        {
            StartupConfig.ApplicationStart();
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="permissions"></param>
        private void SetPermissions(IEnumerable<string> permissions)
        {
            Context.User = new GenericPrincipal(Context.User.Identity, permissions.ToArray());
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private bool ShouldIgnoreRequest()
        {
            string[] reservedPath =
            {
                "/__browserLink",
                "/Scripts",
                "/Content"
            };

            var rawUrl = Context.Request.RawUrl;
            if (reservedPath.Any(path => rawUrl.StartsWith(path, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return
                BundleTable.Bundles.Select(bundle => bundle.Path.TrimStart('~'))
                    .Any(bundlePath => rawUrl.StartsWith(bundlePath, StringComparison.OrdinalIgnoreCase));
        }

        #endregion Private Methods
    }
}
