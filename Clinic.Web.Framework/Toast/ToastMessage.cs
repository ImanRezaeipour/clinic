using System;

namespace Clinic.FrameWork.Toast
{
    /// <summary>
    /// The ToastMessage class is a simple implementation of a message to be shown. Of all options available for a toast message, only a few are implemented here. The class is marked as Serializable because we’ll be storing it in the TempData collection.
    /// </summary>
    [Serializable]
    public class ToastMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ToastType ToastType { get; set; }
        public bool IsSticky { get; set; }
    }
}
