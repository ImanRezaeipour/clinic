using System;

namespace Clinic.Core.Constants
{
    public static class CacheSetting
    {
        public const string Key = "SitemapNodes";
        public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
    }
}