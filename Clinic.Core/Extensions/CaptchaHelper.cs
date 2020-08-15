using System;

namespace Clinic.Core.Extensions
{
    public static class CaptchaHelper
    {
        public static int CreateSalt()
        {
            Random random = new Random();
            return random.Next(1000, 9999);
        }
    }
}