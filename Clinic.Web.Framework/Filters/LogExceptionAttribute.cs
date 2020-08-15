using System;
using System.Web.Mvc;

namespace Clinic.FrameWork.Filters
{
    public class LogExceptionAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}
