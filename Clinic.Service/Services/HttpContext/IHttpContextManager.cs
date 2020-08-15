using System;

namespace Clinic.Service.Services.HttpContext
{
    public interface IHttpContextManager
    {
        Guid CurrentCompanyId();
        string CurrentRequestBrowser();
        string CurrentRequestIp();
        Uri CurrentRequestUrl();
        Guid CurrentRoleId();
        Guid CurrentUserId();
    }
}