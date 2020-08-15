using AutoMapper;
using Clinic.Core.Domains.Patients;
using Clinic.Core.Models.Patient;
using DNTPersianUtils.Core;

namespace Clinic.Core.Profiles
{
    public class PatientProfile : Profile
    {
        #region Public Constructors

        public PatientProfile()
        {
            CreateMap<Patient, PatientViewModel>(MemberList.None)
                .ForMember(dest => dest.BirthDayOn, opt => opt.MapFrom(src => src.BirthDayOn.ToPersianDateTextify()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.ToPersianDateTextify()));
             
            CreateMap<PatientViewModel, Patient>(MemberList.None);

            CreateMap<Patient, PatientCreateViewModel>(MemberList.None);

            CreateMap<PatientCreateViewModel, Patient>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore())
                .ForMember(source => source.AddressId, opt => opt.Ignore());

            CreateMap<Patient, PatientEditViewModel>(MemberList.None);

            CreateMap<PatientEditViewModel, Patient>(MemberList.None)
                .ForMember(source => source.Id, opt => opt.Ignore())
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