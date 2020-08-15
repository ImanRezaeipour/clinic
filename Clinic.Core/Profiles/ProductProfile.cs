using AutoMapper;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.Product;

namespace Clinic.Core.Profiles
{
    public class ProductProfile : Profile
    {
        #region Public Constructors

        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>(MemberList.None);

            CreateMap<ProductViewModel, Product>(MemberList.None);

            CreateMap<Product, ProductCreateViewModel>(MemberList.None);

            CreateMap<ProductCreateViewModel, Product>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore());

            CreateMap<Product, ProductEditViewModel>(MemberList.None);

            CreateMap<ProductEditViewModel, Product>(MemberList.None)
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