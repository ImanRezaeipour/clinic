using AutoMapper;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.ProductBuy;

namespace Clinic.Core.Profiles
{
    public class ProductBuyProfile : Profile
    {
        #region Public Constructors

        public ProductBuyProfile()
        {
            CreateMap<ProductBuy, ProductBuyViewModel>(MemberList.None)
                .ForMember(dest => dest.Title, opt => opt.MapFrom(source => source.Product.Title))
                .ForMember(dest => dest.CompanyTitle, opt => opt.MapFrom(source => source.Seller.CompanyTitle))
                ;

            CreateMap<ProductBuyViewModel, ProductBuy>(MemberList.None)
                ;

            CreateMap<ProductBuy, ProductBuyCreateViewModel>(MemberList.None);

            CreateMap<ProductBuyCreateViewModel, ProductBuy>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore());

            CreateMap<ProductBuy, ProductBuyEditViewModel>(MemberList.None);

            CreateMap<ProductBuyEditViewModel, ProductBuy>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore());
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// </summary>
        public override string ProfileName => GetType().Name;

        #endregion Public Properties

    }
}