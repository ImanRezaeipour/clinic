namespace Clinic.Service.Managers.Attachments
{
    public interface IAttachmentType
    {
        string MimeType
        {
            get;
        }

        string FriendlyName
        {
            get;
        }

        string Extension
        {
            get;
        }
    }

}
