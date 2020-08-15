using System.Web.Mvc;
using Clinic.Service.Services.Locations;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddressController : Controller
    {
        #region Private Fields

        private readonly IAddressService _addressService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressService"></param>
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        #endregion Public Constructors
    }
}