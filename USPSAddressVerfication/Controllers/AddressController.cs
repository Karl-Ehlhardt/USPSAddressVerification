using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using USPSAddressVerfication.Data;
using USPSAddressVerfication.Models;
using USPSAddressVerfication.Service;

namespace USPSAddressVerfication.Controllers
{
    public class AddressController : Controller
    {

        private AddressService CreateAddressService()
        {
            var addressService = new AddressService();
            return addressService;
        }


        public async Task<ActionResult> AddressList()
        {
            AddressService service = CreateAddressService();

            IEnumerable<Address> mymodel = await service.GetAddressesList();

            return View(mymodel);
        }

        //Add method here VVVV
        //GET
        public ActionResult Create()
        {

            return View();
        }

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

        public async Task<ActionResult> Choice()
        {
            AddressService service = CreateAddressService();

            AddressChoice mymodel = await service.GetUserAndVerifiedAddress();

            return View(mymodel);
        }

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