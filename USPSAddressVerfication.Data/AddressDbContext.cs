using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPSAddressVerfication.Data
{
    public class AddressDbContext : DbContext
    {
        // Inherits from DbContext in EntityFramework - this translates between SQL and C#
        public AddressDbContext() : base("DefaultConnection") { }


        // Defines our Restaurants - EntityFramework will convert our SQL db table into a collection of C# objects
        public DbSet<Address> Addresses { get; set; }
    }
}
