using System.Drawing;
using System.Threading.Tasks;
using NReco.VideoInfo;

namespace Clinic.Service.Managers.Video
{
    public interface IVideoManager
    {
        Task CutDownAsync(string sourceFile, int? startTime, int? endTime);
        Task WatermarkMediaAsync(string filePath, string companyName, VideoWatermarkType watermarkType, VideoWatermarkPositionType watermakPosition, string companyLogoFilePath);
        Task GenerateThumbnailAsync(string filePath);
        Task<MediaInfo> ResolveMetadataAsync(string filePath);
        Task WatermarkWithCustomTextAsync(string filePath);
        Task ConvertVideoAsync(string filePath);

        /// <summary>
        /// تبدیل متن به عکس
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="textColor"></param>
        /// <param name="backColor"></param>
        /// <returns></returns>
        System.Drawing.Image DrawText(string text, Font font, Color textColor, Color backColor);
    }
}