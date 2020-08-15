using System;

namespace Clinic.Service.Services.Session
{
    public interface ISessionManager
    {
        string createSessionId();
        ISession getSession(string strSesId);
        ISession removeSession(string strSesId);
    }

    public interface ISession
    {
        Object getAttribute(string strKey);
        void setAttribute(string strKey, Object objValue);
        Object removeAttribute(string strKey);
    }


}
