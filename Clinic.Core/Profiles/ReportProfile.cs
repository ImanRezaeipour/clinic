using AutoMapper;
using Clinic.Core.Domains.Reports;
using Clinic.Core.Models.Report;

namespace Clinic.Core.Profiles
{
    public class ReportProfile : Profile
    {
        #region Public Constructors

        public ReportProfile()
        {
            CreateMap<Report, ReportViewModel>(MemberList.None).ReverseMap();

            CreateMap<Report, ReportCreateViewModel>(MemberList.None).ReverseMap();

            CreateMap<Report, ReportEditViewModel>(MemberList.None).ReverseMap();
        }

        #endregion Public Constructors
    }
}