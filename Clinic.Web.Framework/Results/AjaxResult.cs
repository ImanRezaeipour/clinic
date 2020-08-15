namespace Clinic.FrameWork.Results
{
    /// <summary>
    /// /// 
    /// </summary>
    public class AjaxResult
    {
        #region Public Properties

        public object Data { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }

        #endregion Public Properties



        #region Public Methods

        /// <summary>
        /// /// 
        /// </summary>
        /// <param name="errorStatus"> </param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static AjaxResult Failed(AjaxErrorStatus errorStatus, string errorMessage = null)
        {
            var result = new AjaxResult { Success = false, Data = null, ErrorCode = (int)errorStatus, ErrorMessage = errorMessage };
            return result;
        }

        /// <summary>
        /// /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AjaxResult Succeeded(object data = null)
        {
            var result = new AjaxResult { Success = true, Data = data, ErrorCode = (int)AjaxErrorStatus.None, ErrorMessage = "" };
            return result;
        }

        #endregion Public Methods
    }
}