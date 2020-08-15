using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Models.Document;
using Clinic.Core.Models.DocumentImage;

namespace Clinic.Core.Profiles
{
    public class DocumentProfile : Profile
    {
        #region Public Constructors

        public DocumentProfile()
        {
            CreateMap<Document, DocumentCreateViewModel>(MemberList.None);
            CreateMap<DocumentCreateViewModel, Document>(MemberList.None);

            CreateMap<Document, DocumentEditViewModel>(MemberList.None);
            CreateMap<DocumentEditViewModel, Document>(MemberList.None);

            CreateMap<Document, DocumentViewModel>(MemberList.None);
            CreateMap<DocumentViewModel, Document>(MemberList.None);
        }

        #endregion Public Constructors

        #region Public Properties

        public override string ProfileName => GetType().Name;

        #endregion Public Properties

    }
}