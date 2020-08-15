using System.Threading.Tasks;

namespace Clinic.Service.Managers.Events
{
    public interface IEventHandler<T>
    {
        #region Public Methods

        Task HandleEvent(T eventMessage);

        #endregion Public Methods
    }
}