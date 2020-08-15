using System;

namespace Clinic.Core.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public string ViewName { get; set; }

        public ValidationException(string message, string viewName = null, string actionName = null) : base(message)
        {
            if (viewName != null)
                Data.Add("viewName", viewName);

            if (actionName != null)
                Data.Add("actionName", actionName);
        }
    }
}