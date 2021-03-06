﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USPSAddressVerification.Data;

namespace USPSAddressVerification.Models
{
    public class AddressChoice
    {
        public Address Address { get; set; }

        public AddressUSPS AddressUSPS { get; set; }

        public AddressChoice(Address address, AddressUSPS addressUSPS)
        {
            Address = address;
            AddressUSPS = addressUSPS;
        }
    }
}
