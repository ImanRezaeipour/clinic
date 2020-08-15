namespace Clinic.FrameWork.Toast
{
    /// <summary>
    /// ToastType is just an enum containing all the different toast types that exists. The enum values are written with with capitalization here but later on these values will be transformed into lower case values to match the toastType object values in the Toastr library.
    /// </summary>
    public enum ToastType
    {
        Error,
        Info,
        Success,
        Warning
    }
}
