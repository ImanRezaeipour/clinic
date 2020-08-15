using AutoMapper;
using Clinic.Core.Domains.Presenters;
using Clinic.Core.Models.Presenter;

namespace Clinic.Core.Profiles
{
    class PresenterProfile : Profile
    {
        public PresenterProfile()
        {
            CreateMap<Presenter, PresenterCreateViewModel>(MemberList.None);
            CreateMap<PresenterCreateViewModel, Presenter>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore())
                .ForMember(source => source.AddressId, opt => opt.Ignore());

            CreateMap<Presenter, PresenterViewModel>(MemberList.None);
            CreateMap<PresenterViewModel, Presenter>(MemberList.None);

            CreateMap<Presenter, PresenterEditViewModel>(MemberList.None);
            CreateMap<PresenterEditViewModel, Presenter>()
                .ForMember(source => source.Id, opt => opt.Ignore())
                .ForMember(source => source.AddressId, opt => opt.Ignore());

        }

        public override string ProfileName => GetType().Name;

    }
}
