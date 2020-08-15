using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Domains.Sales;
using Clinic.Core.Models.DocumentSaleProduct;

namespace Clinic.Core.Profiles
{
    public class DocumentSaleProductProfile : Profile
    {
        #region Public Constructors

        public DocumentSaleProductProfile()
        {
            CreateMap<SaleProduct, DocumentSaleProductCreateViewModel>(MemberList.None);
            CreateMap<DocumentSaleProductCreateViewModel, SaleProduct>(MemberList.None);

            CreateMap<SaleProduct, DocumentSaleProductEditViewModel>(MemberList.None);
            CreateMap<DocumentSaleProductEditViewModel, SaleProduct>(MemberList.None);

            CreateMap<SaleProduct, DocumentSaleProductViewModel>(MemberList.None);
            CreateMap<DocumentSaleProductViewModel, SaleProduct>(MemberList.None);
        }

        #endregion Public Constructors

        #region Public Properties

        public override string ProfileName => GetType().Name;

        #endregion Public Properties

    }
}