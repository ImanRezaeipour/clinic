using AutoMapper;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Expertise;

namespace Clinic.Core.Profiles
{
    public class ExpertiseProfile : Profile
    {
        #region Public Constructors

        public ExpertiseProfile()
        {
            CreateMap<Expertise, ExpertiseViewModel>(MemberList.None).ReverseMap();
            

            CreateMap<Expertise, ExpertiseCreateViewModel>(MemberList.None);

            CreateMap<ExpertiseCreateViewModel, Expertise>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore());

            CreateMap<Expertise, ExpertiseEditViewModel>(MemberList.None).ReverseMap();

            CreateMap<SelectListItem, Expertise>(MemberList.None).ReverseMap();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// </summary>
        public override string ProfileName => GetType().Name;

        #endregion Public Properties

    }
}