using System.ComponentModel;

namespace Clinic.Core.Types

{
    public enum ImageProcessType
    {
        [Description("")]
        Nan = 0,

        [Description("")]
        ProductImages = 1,

        [Description("")]
        ProductImage = 6,

        [Description("")]
        CompanyImages = 2,

        [Description("")]
        CompanyCoverFileName = 3,

        [Description("")]
        LogoFileName = 4,

        [Description("")]
        UserMetaAvatorFileName = 5,

        [Description("")]
        Attachment = 7
    }
}