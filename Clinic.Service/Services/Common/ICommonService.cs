using System;
using System.Threading.Tasks;

namespace Clinic.Service.Services.Common
{
    public interface ICommonService
    {
        Task<Guid?> GetUserIdAsync(bool isCurrentUser, Guid? userId);
        int RandomNumberByCount(int min, int max);
    }
}