﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPSAddressVerification.Models
{
    public class AddressUser
    {
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
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Please enter a 5 diget zipcode")]
        [Display(Name = "5 Digit Zip Code")]
        public string Zip5 { get; set; }

        //These are the dorpdown choices to reduce errors
        public enum States
        {

            AL,
            AK,
            AZ,
            AR,
            CA,
            CO,
            CT,
            DC,
            DE,
            FL,
            GA,
            HI,
            ID,
            IL,
            IN,
            IA,
            KS,
            KY,
            LA,
            ME,
            MD,
            MA,
            MI,
            MN,
            MS,
            MO,
            MT,
            NE,
            NV,
            NH,
            NJ,
            NM,
            NY,
            NC,
            ND,
            OH,
            OK,
            OR,
            PA,
            RI,
            SC,
            SD,
            TN,
            TX,
            UT,
            VT,
            VA,
            WA,
            WV,
            WI,
            WY
        }
    }
}
