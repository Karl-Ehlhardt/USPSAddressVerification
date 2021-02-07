using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using USPSAddressVerfication.Data;
using USPSAddressVerfication.Models;

namespace USPSAddressVerfication.Service
{
    public class AddressService
    {

        //private context
        private AddressDbContext _context = new AddressDbContext();

        //Get Verified Address, return both
        public async Task<bool> GetVerifiedAddress(AddressUserInput model)
        {
            XDocument request = new XDocument(
                new XElement("AddressValidateRequest", 
                        new XAttribute("USERID", "XXX"),
                    new XElement("Revision", "1"),
                    new XElement("Address",
                            new XAttribute("ID", "0"),
                        new XElement("Address1", model.Address1),
                        new XElement("Address2", model.Address2),
                        new XElement("City", model.City),
                        new XElement("State", model.State),
                        new XElement("Zip5", model.Zip5),
                        new XElement("Zip4", "")
                        )
                    )

                );
            return await ;
        }

        ////Get Verified Address
        //public async Task<bool> CreateDogBasic(DogBasicCreate model)
        //{
        //    DogBasic dogBasic =
        //        new DogBasic()
        //        {
        //            DogName = model.DogName,
        //            Breed = model.Breed,
        //            Weight = model.Weight
        //        };

        //    _context.DogBasics.Add(dogBasic);
        //    return await _context.SaveChangesAsync() == 1;
        //}

        //Get dogBasic by id
        //public async Task<DogBasicDetails> GetDogBasicById([FromUri] int id)
        //{
        //    var query =
        //        await
        //        _context
        //        .DogBasics
        //        .Where(q => q.DogBasicId == id)
        //        .Select(
        //            q =>
        //            new DogBasicDetails()
        //            {
        //                DogBasicId = q.DogBasicId,
        //                DogName = q.DogName,
        //                Breed = q.Breed,
        //                Weight = q.Weight
        //            }).ToListAsync();
        //    return query[0];
        //}

        //===================HELPERS========//
        public static string GetXMLElement(XElement element, string name)
        {
            var el = element.Element(name);
            if (el != null)
            {
                return el.Value;
            }
            return "";
        }
    }
}
