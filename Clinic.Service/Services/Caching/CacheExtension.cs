using System;
using System.Web;
using System.Web.Caching;

namespace Clinic.Service.Services.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public static class CacheExtension
    {
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext">    </param>
        /// <param name="key">            </param>
        /// <param name="data">           </param>
        /// <param name="durationMinutes"></param>
        public static void CacheInsert(this HttpContextBase httpContext, string key, object data, int durationMinutes)
        {
            if (data == null)
                return; 

            httpContext.Cache.Add(key, data, null, DateTime.UtcNow.AddMinutes(durationMinutes), TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="key">        </param>
        /// <param name="data">       </param>
        /// <param name="seconds">    </param>
        public static void CacheInsertWithSeconds(this HttpContextBase httpContext, string key, object data, int seconds)
        {
            if (data == null)
                return;

            httpContext.Cache.Add(key, data, null, DateTime.UtcNow.AddMinutes(seconds), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Low, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpContext"></param>
        /// <param name="key">        </param>
        /// <returns></returns>
        public static T CacheRead<T>(this HttpContextBase httpContext, string key)
        {
            var data = httpContext.Cache[key];
            if (data != null)
                return (T)data;
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="key">        </param>
        /// <returns></returns>
        public static object CacheRead(this HttpContextBase httpContext, string key)
        {
            var data = httpContext.Cache[key];
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public static void DisableBrowserCache(this HttpContextBase httpContext)
        {
            httpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            httpContext.Response.Cache.SetValidUntilExpires(false);
            httpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            httpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            httpContext.Response.Cache.SetNoStore();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="key">        </param>
        public static void InvalidateCache(this HttpContextBase httpContext, string key)
        {
            httpContext.Cache.Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void InvalidateChildActionsCache()
        {
            //OutputCacheAttribute.ChildActionCache = new System.Runtime.Caching.MemoryCache(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public static void InvalidateOutPutCache(string url)
        {
            HttpResponse.RemoveOutputCacheItem(url);
        }

        #endregion Public Methods
    }
}