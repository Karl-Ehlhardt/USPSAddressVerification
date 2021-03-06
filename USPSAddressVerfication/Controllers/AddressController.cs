﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using USPSAddressVerification.Data;
using USPSAddressVerification.Models;
using USPSAddressVerification.Service;

namespace USPSAddressVerification.Controllers
{
    public class AddressController : Controller
    {

        private AddressService CreateAddressService()
        {
            var addressService = new AddressService();
            return addressService;
        }

        //List all addreses
        public async Task<ActionResult> AddressList()
        {
            AddressService service = CreateAddressService();

            IEnumerable<Address> mymodel = await service.GetAddressesList();

            return View(mymodel);
        }

        //GET
        public ActionResult Create()
        {

            return View();
        }

        //Creates the address in the database then directs to the Coice screen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddressUser model)
        {
            if (!ModelState.IsValid) return View(model);

            AddressService service = CreateAddressService();

            if (await service.CreateAddress(model))
            {
                return RedirectToAction($"Choice");
            };

            ModelState.AddModelError("", "Address could not be added");

            return View(model);
        }

        //Loads to display both the user entered address and 
        public async Task<ActionResult> Choice()
        {
            AddressService service = CreateAddressService();

            AddressChoice mymodel = await service.GetUserAndVerifiedAddress();

            return View(mymodel);
        }

        //Returns the AddressUSPS model only, along with weather it is being saved, then the service performs the requested action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Choice([Bind(Prefix = "AddressUSPS")] AddressUSPS model)
        {
            if (!ModelState.IsValid) return RedirectToAction($"Choice");

            AddressService service = CreateAddressService();

            if (await service.FinalChoice(model))
            {
                return RedirectToAction($"Index", "Home");
            };

            ModelState.AddModelError("", "Address could not be finalized");

            return RedirectToAction($"Choice");
        }

    }
}