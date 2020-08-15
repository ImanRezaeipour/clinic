using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Address
{
    public class AddressViewModel : BaseViewModel
    {
        public Guid Id { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Street { get; set; }

       public string PostalCode { get; set; }

        public string Extra { get; set; }


    }
}