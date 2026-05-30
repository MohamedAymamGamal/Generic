using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
    public record OrderDto
    {
        public int deliveryMethodId { get; set; }

        public string BasketId { get; set; }
        public ShipAddressDto shipAddress { get; set; }


    }

    public record ShipAddressDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
    }
}
