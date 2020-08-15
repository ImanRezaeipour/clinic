using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Clinic.FrameWork.Configs;
using RazorGenerator.Mvc;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(RazorGeneratorMvcStart), "Start")]

namespace Clinic.FrameWork.Configs
{
    public static class RazorGeneratorMvcStart
    {
        #region Public Methods

        public static void Start()
        {
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly)
            {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages.
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }

        #endregion Public Methods
    }
}