using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Clinic.Core.Types;

namespace Clinic.Service.Managers.File
{

    public interface IFileManager
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<FileModel> CreateAsync(string name, string path);

        /// <summary>
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="path">     </param>
        /// <returns></returns>
        Task<IList<FileModel>> DeleteAsync(string[] fileNames, string path = null);

        /// <summary>
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<ContentResult> GetAsContentResultAsync(IList<FileModel> files);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<IList<FileModel>> GetListAsync(string path);

        /// <summary>
        /// </summary>
        /// <param name="files"></param>
        /// <param name="path"> </param>
        /// <returns></returns>
        Task<IList<FileModel>> SaveAsync(IEnumerable<HttpPostedFileBase> files, string path = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<FilePathResult> GetFileFromThumbAsync(string path);

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<FileModel> SaveFromUploaderAsync(HttpPostedFileBase file, string path = null);

        #endregion Public Methods

        Task<IList<FileModel>> SaveImageAsync(IEnumerable<HttpPostedFileBase> files, string path = null, ImageProcessType imageType = ImageProcessType.Nan);
        Task<IList<FileModel>> SaveVideoAsync(IEnumerable<HttpPostedFileBase> files, string path = null);
        Task<IList<FileModel>> SaveAttachmentAsync(IEnumerable<HttpPostedFileBase> files, string path = null);
    }
}