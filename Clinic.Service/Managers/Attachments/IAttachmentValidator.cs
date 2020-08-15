using System.Threading.Tasks;
using System.Web;

namespace Clinic.Service.Managers.Attachments
{
    public interface IAttachmentValidator
    {
        Task<string> GetFormatAsync(HttpPostedFileBase file);
        Task<string> GetSizeAsync(HttpPostedFileBase file);
    }
}