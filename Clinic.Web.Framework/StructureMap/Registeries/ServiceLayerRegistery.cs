using Clinic.Service.Services;
using Clinic.Service.Services.Logs;
using Clinic.Service.Services.Users;
using StructureMap;

namespace Clinic.FrameWork.StructureMap.Registeries
{
    /// <summary>
    /// </summary>
    public class ServiceLayerRegistery : Registry
    {

        /// <summary>
        /// </summary>
        public ServiceLayerRegistery()
        {
            Policies.SetAllProperties(y => { y.OfType<IActivityLogService>(); });
            Scan(scanner =>
            {
                scanner.Assembly(typeof(UserService).Assembly);
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<UserService>();
            });
        }
    }
}