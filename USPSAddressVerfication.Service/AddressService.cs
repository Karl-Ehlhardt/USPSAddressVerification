﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using USPSAddressVerfication.Data;
using USPSAddressVerfication.Models;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Linq;
using System.Data.Entity;

namespace USPSAddressVerfication.Service
{
    public class AddressService
    {

        //private context
        private AddressDbContext _context = new AddressDbContext();

        //Create an address from User input, will change to the final choice later
        public async Task<bool> CreateAddress(AddressUser model)
        {
            Address address =
                new Address()
                {
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    State = model.State,
                    Zip5 = model.Zip5
                };

            _context.Addresses.Add(address);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get Verified Address, return both
        public async Task<AddressChoice> GetVerifiedAddress()
        {
            Address model =
                        await
                    _context
                    .Addresses.OrderByDescending(p => p.AddressId)
                    .FirstOrDefaultAsync();

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
                        new XElement("Zip5", model.Zip5.ToString()),
                        new XElement("Zip4", "")
                        )
                    )
                );
            XDocument xDoc = new XDocument();
            AddressUSPS uSPS = new AddressUSPS();

            try
            {
                string url = "https://secure.shippingapis.com/ShippingAPI.dll?API=Verify&XML=" + request;

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string xml = await content.ReadAsStringAsync();
                            xDoc = XDocument.Parse(xml);
                        }
                    }
                }
                
                foreach (XElement el in xDoc.Descendants("Address"))
                {
                    uSPS.Address1 = GetXMLElement(el, "Address1");
                    uSPS.Address2 = GetXMLElement(el, "Address2");
                    uSPS.City = GetXMLElement(el, "City");
                    uSPS.State = GetXMLElement(el, "State");
                    uSPS.Zip5 = GetXMLElement(el, "Zip5");
                    uSPS.Zip4 = GetXMLElement(el, "Zip4");
                    uSPS.Picked = false;
                }
            }
            catch (WebException)
            {
                return new AddressChoice(model, null);
            }

            return new AddressChoice(model, uSPS);
        }

        //Update Address to the final choice
        public async Task<bool> FinalChoice(int id, AddressUSPS model)
        {
            if (!model.Picked)
            {
                return true;
            }

            Address address =
                        await
                    _context
                    .Addresses
                    .SingleAsync(a => a.AddressId == id);

            address.Address1 = model.Address1;
            address.Address2 = model.Address2;
            address.City = model.City;
            address.State = model.State;
            address.Zip5 = model.Zip5;
            address.Zip4 = model.Zip4;

            return await _context.SaveChangesAsync() == 1;
        }


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
