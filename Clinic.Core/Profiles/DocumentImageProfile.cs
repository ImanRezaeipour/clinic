using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Models.DocumentImage;

namespace Clinic.Core.Profiles
{
    public class DocumentImageProfile : Profile
    {
        #region Public Constructors

        public DocumentImageProfile()
        {
            CreateMap<DocumentImage, DocumentImageCreateViewModel>(MemberList.None);
            CreateMap<DocumentImageCreateViewModel, DocumentImage>(MemberList.None);

            CreateMap<DocumentImage, DocumentImageEditViewModel>(MemberList.None);
            CreateMap<DocumentImageEditViewModel, DocumentImage>(MemberList.None);

            CreateMap<DocumentImage, DocumentImageViewModel>(MemberList.None);
            CreateMap<DocumentImageViewModel, DocumentImage>(MemberList.None);
        }

        #endregion Public Constructors

        #region Public Properties

        public override string ProfileName => GetType().Name;

        #endregion Public Properties

    }
}