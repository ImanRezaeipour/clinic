using System;
using System.Web.Mvc;
using Clinic.Core.Models.Statistic;
using Clinic.Core.Types;
using Clinic.Core.Utilities.Http;
using Clinic.Service.Services.Statistics;
using StructureMap.Attributes;

namespace Clinic.FrameWork.Filters
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class StatisticAttribute:ActionFilterAttribute
    {
        private IStatisticService _statisticService;

        [SetterProperty]
        public IStatisticService StatisticService
        {
            set { _statisticService = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var statisticViewmodel = new StatisticCreateViewModel
            {
                ControllerName = filterContext.RequestContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RequestContext.RouteData.Values["action"].ToString(),
                ViewedOn = DateTime.Now,
                Referrer = filterContext.RequestContext.HttpContext.Request.UrlReferrer?.ToString(),
                Audit = AuditType.Create,
                IpAddress = filterContext.HttpContext.Request.GetIp(),
                UserAgent = filterContext.HttpContext.Request.GetBrowser(),
                UserOs = filterContext.HttpContext.Request.GetOs()
            };
            _statisticService.CreateByViewModelAsync(statisticViewmodel);

            base.OnActionExecuting(filterContext);
        }
    }
}
