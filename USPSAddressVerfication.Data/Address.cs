using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPSAddressVerfication.Data
{

    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Display(Name = "Appartment/Suite")]
        public string Address1 { get; set; }//Appartment/Suite number

        [Display(Name = "Address")]
        [Required]
        public string Address2 { get; set; }//street address

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip5 { get; set; }

        public string Zip4 { get; set; }
    }


}
