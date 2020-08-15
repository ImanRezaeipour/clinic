using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Doctor;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Contracts;
using Clinic.Service.HttpContext;

namespace Clinic.Service.Services.Common
{
    /// <summary>
    /// </summary>
    public abstract class BaseService : Service
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Doctor> _doctors;
        private readonly IHttpContextManager _httpContextManager;

        protected BaseService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _doctors = unitOfWork.Set<Doctor>();
        }
        
}