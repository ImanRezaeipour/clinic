using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Role
{
    /// <summary>
    /// </summary>
    public class PermissionViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelect { get; set; }
    }
}