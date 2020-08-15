using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Clinic.Core.Extensions;
using Clinic.Core.Helpers;
using Clinic.Service.Services.Image;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Clinic.Service.Services.File
{
    /// <summary>
    /// </summary>
    public class FileManager : IFileManager
    {
        #region Public Methods

        public static byte[] ConvertToByteArrary(Stream stream, int length)
        {
            var fileData = new byte[length];
            stream.Read(fileData, 0, Convert.ToInt32(length));
            return fileData;
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<FileModel> CreateAsync(string name, string path)
        {
            if (name == null)
                return null;
            var directory = Path.Combine(path, name);
            Directory.CreateDirectory(directory);
            return new FileModel()
            {
                Name = name,
                Type = FileConst.DirectoryType,
                Extension = "",
                Size = 0,
                Path = path
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="path">     </param>
        /// <returns></returns>
        public async Task<IList<FileModel>> DeleteAsync(string[] fileNames, string path = null)
        {
            var fileList = new List<FileModel>();
            if (fileNames == null)
                return fileList;
            foreach (var fileName in fileNames)
            {
                if (path == null)
                    path = FileConst.ImagesWebPath;
                var pathToDelete = Path.Combine(path, fileName);
                var attr = System.IO.File.GetAttributes(pathToDelete);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Directory.Delete(pathToDelete, recursive: true);
                    var fileDeleted = new FileModel()
                    {
                        Name = fileName,
                        Type = FileConst.DirectoryType,
                        Extension = "",
                        Size = 0,
                        Path = path
                    };
                    fileList.Add(fileDeleted);
                }
                else
                {
                    System.IO.File.Delete(pathToDelete);
                    var fileDeleted = new FileModel()
                    {
                        Name = fileName,
                        Type = FileConst.FileType,
                        Extension = "",
                        Size = 0,
                        Path = path
                    };
                    fileList.Add(fileDeleted);
                }
            }
            return fileList;
        }

        /// <summary>
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ContentResult> GetAsContentResultAsync(IList<FileModel> files)
        {
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(files, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }),
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<FilePathResult> GetFileFromThumbAsync(string path)
        {
            // Process
            var name = Path.GetFileName(path);
            if (name == null)
                return null;
            var directory = Path.GetDirectoryName(path);
            if (directory == null)
                return null;
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(FileConst.ThumbPath), directory, name);

            //  Result
            var result = await GetFileAsync(filePath);
            return result != null ? new FilePathResult(result.Path, "image/png") : null;
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<IList<FileModel>> GetListAsync(string path)
        {
            var files = new DirectoryInfo(path).GetFiles()
                .Select(fileInfo => new FileModel
                {
                    Name = fileInfo.Name,
                    Size = fileInfo.Length,
                    Type = FileConst.FileType,
                    Extension = "",
                    Path = path
                }).ToList();
            var folders = new DirectoryInfo(path).GetDirectories()
                .Select(directoryInfo => new FileModel
                {
                    Name = directoryInfo.Name,
                    Size = 0,
                    Type = FileConst.DirectoryType,
                    Extension = "",
                    Path = path
                }).ToList();
            return files.Union(folders).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="files"></param>
        /// <param name="path"> </param>
        /// <returns></returns>
        public async Task<IList<FileModel>> SaveAsync(IEnumerable<HttpPostedFileBase> files, string path = null)
        {
            var fileList = new List<FileModel>();
            if (files == null)
                return fileList;
            foreach (var file in files)
            {
                if (path == null)
                    path = FileConst.ImagesWebPath;
                var extension = Path.GetExtension(file.FileName);
                var size = 0;
                var name = SequentialGuidGenerator.NewSequentialGuid() + extension;
                var pathToSave = Path.Combine(path, name);
                file.SaveAs(pathToSave);
                var fileSaved = new FileModel()
                {
                    Id = SequentialGuidGenerator.NewSequentialGuid(),
                    Name = name,
                    Type = FileConst.FileType,
                    Extension = extension,
                    Size = size,
                    Path = path
                };
                fileList.Add(fileSaved);
            }
            return fileList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<FileModel> SaveFromUploaderAsync(HttpPostedFileBase file, string path = null)
        {
            // Check
            if (file == null)
                return null;

            // Process
            var extension = Path.GetExtension(file.FileName);
            var id = SequentialGuidGenerator.NewSequentialGuid();
            var name = id + extension;
            var webpath = path != null ? System.Web.HttpContext.Current.Server.MapPath(FileConst.UploadPath.PlusWord(path)) : System.Web.HttpContext.Current.Server.MapPath(FileConst.UploadPath);
            var web = await ConvertToByteArrayAsync(file);
            // var waterMarked = ImageManager.AddWaterMark(thumb, "Arezoo");
            var result = await CreateFileAsync(web, name, webpath);

            var thumbPath = path != null ? System.Web.HttpContext.Current.Server.MapPath(FileConst.ThumbPath.PlusWord(path)) : System.Web.HttpContext.Current.Server.MapPath(FileConst.ThumbPath);

            var thumbFile = await ConvertToByteArrayAsync(file);
            var resized = thumbFile.ResizeImageFile(100, 100);
           

            var thumbResult = await CreateFileAsync(resized, name, thumbPath);

            //  Result
            return thumbResult != null ? thumbResult : null;
        }

        #endregion Public Methods



        #region Private Methods

        private async Task<Byte[]> ConvertToByteArrayAsync(HttpPostedFileBase file)
        {
            Byte[] destination = new Byte[file.ContentLength];
            file.InputStream.Position = 0;
            file.InputStream.Read(destination, 0, file.ContentLength);
            return destination;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<FileModel> CreateFileAsync(byte[] file, string fileName, string path)
        {
            //  Check
            if (file == null)
                return null;

            if (path == string.Empty)
                return null;

            //  Process
            var pathToSave = Path.Combine(path, fileName);
            System.IO.File.WriteAllBytes(pathToSave, file);

            //  Result
            var result = new FileModel()
            {
                Id = Guid.Parse(Path.GetFileNameWithoutExtension(fileName)),
                Type = FileConst.FileType,
                Name = fileName,
                Extension = Path.GetExtension(fileName),
                Path = pathToSave,
                Size = 0
            };
            return result;
        }

        private async Task<FileModel> DeleteFileAsync(string fileName, string filePath)
        {
            // Check
            if (fileName == string.Empty)
                return null;
            if (filePath == string.Empty)
                return null;

            // Process
            var fileFullName = Path.Combine(filePath, fileName);
            if (System.IO.File.Exists(fileFullName))
            {
                System.IO.File.Delete(fileFullName);
                var result = new FileModel()
                {
                    Id = Guid.Parse(Path.GetFileNameWithoutExtension(fileName)),
                    Path = filePath,
                    Name = fileName,
                    Type = FileConst.FileType,
                    Size = 0,
                    Extension = Path.GetExtension(fileName)
                };
                return result;
            }
            return null;
        }

        //private async Task<System.IO.File> GetFileAsync(string fileName, string filePath)
        //{
        //    // Check
        //    if (fileName == string.Empty)
        //        return null;
        //    if (filePath == string.Empty)
        //        return null;

        //    // Process
        //    var fileFullName = Path.Combine(filePath, fileName);
        //    if (System.IO.File.Exists(fileFullName))
        //    {
        //        var file = System.IO.File.
        //    }
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private async Task<FileModel> GetFileAsync(string filePath)
        {
            //  Result
            return System.IO.File.Exists(filePath)
                ? new FileModel()
                {
                    Name = Path.GetFileName(filePath),
                    Extension = Path.GetExtension(filePath),
                    Path = filePath,
                    Type = FileConst.FileType,
                    Id = Guid.Parse(Path.GetFileNameWithoutExtension(filePath)),
                    Size = 0
                }
                : null;
        }

        #endregion Private Methods
    }
}