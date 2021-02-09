using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPSAddressVerification.Data
{
    public class AddressDbContext : DbContext
    {
        public AddressDbContext() : base("DefaultConnection") { }

        public DbSet<Address> Addresses { get; set; }
    }
}
