using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPSAddressVerfication.Models
{
    public class AddressUSPS
    {
        [Display(Name = "Appartment/Suite")]
        public string Address1 { get; set; }//Appartment/Suite number

        [Display(Name = "Address")]
        public string Address2 { get; set; }//street address

        public string City { get; set; }

        public string State { get; set; }

        public string Zip5 { get; set; }

        public string Zip4 { get; set; }

        public bool Picked { get; set; }
    }
}
