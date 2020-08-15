using Clinic.Service.Services;
using Clinic.Service.Services.Transaction;
using Clinic.Service.Services.Users;
using StructureMap;

namespace Clinic.FrameWork.StructureMap.Registeries
{
    /// <summary>
    /// </summary>
    public class TaskRegistry : Registry
    {

        /// <summary>
        /// </summary>
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.Assembly(typeof(UserService).Assembly);
                scan.AddAllTypesOf<IRunAfterEachRequest>();
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartUp>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
            });
        }
    }
}