using AutoMapper;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.ProductSeller;

namespace Clinic.Core.Profiles
{
   public class ProductSellerProfile :Profile
    {
        public ProductSellerProfile()
        {
            CreateMap<ProductSellerViewModel, ProductSeller>(MemberList.None);
            CreateMap<ProductSeller, ProductSellerViewModel>(MemberList.None);

            CreateMap<ProductSeller, ProductSellerCreateViewModel>(MemberList.None);
            CreateMap<ProductSellerCreateViewModel, ProductSeller>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore());

            CreateMap<ProductSeller, ProductSellerViewModel>(MemberList.None);
            CreateMap<ProductSellerViewModel, ProductSeller>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore());
        }
    }
}
