using Clinic.Core.Domains.Doctors;
using Clinic.Core.Models.Doctor;
using AutoMapper;

namespace Clinic.Core.Profiles
{
    public class DoctorProfile : Profile
    {
        #region Public Constructors

        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorViewModel>(MemberList.None)
                .ForMember(dest => dest.ExpertiseName, opt => opt.MapFrom(sour => sour.Expertise.ExpertiseName));

            CreateMap<DoctorViewModel, Doctor>(MemberList.None)
                ;

            CreateMap<Doctor, DoctorCreateViewModel>(MemberList.None);

            CreateMap<DoctorCreateViewModel, Doctor>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore())
                .ForMember(source => source.ExpertiseId, opt => opt.Ignore())
                .ForMember(source => source.AddressId, opt => opt.Ignore());

            CreateMap<Doctor, DoctorEditViewModel>(MemberList.None);

            CreateMap<DoctorEditViewModel, Doctor>(MemberList.None)
                .ForMember(source => source.AddressId, opt => opt.Ignore());
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// </summary>
        public override string ProfileName => GetType().Name;

        #endregion Public Properties

    }
}