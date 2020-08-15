using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Domains.Sales;
using Clinic.Core.Models.DocumentSale;

namespace Clinic.Core.Profiles
{
    public class DocumentSaleProfile : Profile
    {
        #region Public Constructors

        public DocumentSaleProfile()
        {
            CreateMap<Sale, DocumentSaleCreateViewModel>(MemberList.None);
            CreateMap<DocumentSaleCreateViewModel, Sale>(MemberList.None);

            CreateMap<Sale, DocumentSaleEditViewModel>(MemberList.None);
            CreateMap<DocumentSaleEditViewModel, Sale>(MemberList.None);

            CreateMap<Sale, DocumentSaleViewModel>(MemberList.None);
            CreateMap<DocumentSaleViewModel, Sale>(MemberList.None);
        }

        #endregion Public Constructors

        #region Public Properties

        public override string ProfileName => GetType().Name;

        #endregion Public Properties

    }
}