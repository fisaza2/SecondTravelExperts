using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelExpertWeb.App_Code
{
    public class CustomerPurchasedPackages
    {
        //setting up package properties ****************FABIAN EDITED 2017-07-18
        public CustomerPurchasedPackages() { }

        public int CustomerId { get; set; }
        public int PackageId { get; set; }
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Destination { get; set; }
        public float TravelerCount { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Total_Package { get; set; }
        public decimal Total { get; set; }

    }
}