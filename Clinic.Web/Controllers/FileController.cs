using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Clinic.Data.DbContexts;
using Clinic.FrameWork.Filters;
using Clinic.FrameWork.Results;
using Clinic.Service.Services.File;

namespace Clinic.Web.Controllers
{
    /// <summary>
    /// </summary>
    public partial class FileController : BaseController
    {

        #region Private Fields

        private readonly IFileManager _fileManager;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork"> </param>
        /// <param name="fileManager"></param>
        public FileController(IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> CreateFromUpload(string name, string path)
        {
            //  Result
            var webPath = Path.Combine(Server.MapPath(FileConst.UploadPath), path);
            var folder = await _fileManager.CreateAsync(name, webPath);
            var thumbPath = Path.Combine(Server.MapPath(FileConst.ThumbPath), path);
            var thumbFolder = await _fileManager.CreateAsync(name, thumbPath);
            return await _fileManager.GetAsContentResultAsync(new List<FileModel> { folder });
        }

        /// <summary>
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<JsonResult> DeleteFromImageWeb(string[] fileNames)
        {
            //  Result
            var path = Server.MapPath(FileConst.ImagesWebPath);
            return Json(await _fileManager.DeleteAsync(fileNames, path), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<JsonResult> DeleteFromUpload(string[] fileNames)
        {
            //  Result
            var path = Server.MapPath(FileConst.UploadPath);
            return Json(await _fileManager.DeleteAsync(fileNames, path), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> GetFileFromThumb(string path)
        {
            //  Result
            var result = await _fileManager.GetFileFromThumbAsync(path);
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> GetFileFromUpload(string path)
        {
            //  Result
            var name = Path.GetFileName(path);
            if (name == null)
                return null;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
                return null;
            path = Path.Combine(Server.MapPath(FileConst.UploadPath), directory, name);
            return File(path, "image/png");
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> ListFromUpload(string path)
        {
            //  Result
            path = Path.Combine(Server.MapPath(FileConst.UploadPath), path);
            var files = await _fileManager.GetListAsync(path);
            return await _fileManager.GetAsContentResultAsync(files);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> Remove()
        {
            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<JsonResult> SaveFromAttachment(IEnumerable<HttpPostedFileBase> files)
        {
            //  Result
            var path = Server.MapPath(FileConst.AttachmentPath);
            return Json(await _fileManager.SaveAsync(files, path), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<JsonResult> SaveFromImageWeb(IEnumerable<HttpPostedFileBase> files)
        {
            //  Result
            var path = Server.MapPath(FileConst.ImagesWebPath);
            var result = await _fileManager.SaveAsync(files, path);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<JsonResult> SaveFromUpload(IEnumerable<HttpPostedFileBase> files)
        {
            //  Result
            var path1 = Server.MapPath(FileConst.UploadPath);
            return Json(await _fileManager.SaveAsync(files, path1), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual async Task<JsonResult> SaveFromUploader(HttpPostedFileBase file, string path)
        {
            //  Check
            if (file == null)
                return Json(AjaxResult.Failed(AjaxErrorStatus.BadRequest), JsonRequestBehavior.AllowGet);

            //  Result
            var result = await _fileManager.SaveFromUploaderAsync(file, path);
            var kendo = new { result.Name, result.Size, result.Type };
            return Json(kendo, JsonRequestBehavior.AllowGet);
        }

        #endregion Public Methods

    }
}