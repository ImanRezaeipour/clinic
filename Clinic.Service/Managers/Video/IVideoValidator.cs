using System.Threading.Tasks;

namespace Clinic.Service.Managers.Video
{
    public interface IVideoValidator
    {
        Task GetFormatAsync(string file);
    }
}
