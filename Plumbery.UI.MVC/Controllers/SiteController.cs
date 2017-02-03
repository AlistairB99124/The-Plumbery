using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using Plumbery.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Plumbery.UI.MVC.Controllers {
    [Authorize]
    public class SiteController : Controller {
        /// <summary>
        /// Site service
        /// </summary>
        private ISiteService _siteService;
        /// <summary>
        /// Initialise Site Services
        /// </summary>
        /// <param name="siteService"></param>
        public SiteController(ISiteService siteService) {
            this._siteService = siteService;
        }

        /// <summary>
        /// Get list of all sites
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            List<Site> sites = _siteService.GetAll().ToList();
            return View(sites);
        }

        public ActionResult Create() {
            ViewBag.CountrySelect = new SelectList(Country.GetCountries(), "Code", "Name", "ZAF");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateSiteViewModels model) {
            if (ModelState.IsValid) {
                Location location = new Location {
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    Country = model.Country,
                    Postal_Code = model.Postal_Code,
                    Province = model.Province
                };
                Site site = new Site {
                    Abbr = model.Abbr,
                    Location = location,
                    Name = model.Name
                };
                var result = await _siteService.Add(site,location);
                if (result > 0) {
                    return RedirectToAction("Index","Site",null);
                }
            }
            ViewBag.CountrySelect = new SelectList(Country.GetCountries(), "Code", "Name", "RSA");
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? Id) {
            if (Id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = _siteService.Get(Id);
            if (site == null) {
                return HttpNotFound();
            }
            var countries = Country.GetCountries();
            EditSiteViewModels model = new EditSiteViewModels {
                Abbr = site.Abbr,
                Name = site.Name,
                Address1 = site.Location.Address1,
                Address2 = site.Location.Address2,
                City = site.Location.City,
                Country = site.Location.Country,
                LocationId = site.LocationId,
                Postal_Code = site.Location.Postal_Code,
                Province = site.Location.Province,
                SiteId = site.Id
            };
            ViewBag.CountrySelect = new SelectList(countries, "Code", "Name", countries.Where(x => x.Name == site.Location.Country).FirstOrDefault().Code);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSiteViewModels model) {

            Site site = _siteService.Get(model.SiteId);
            Location location = _siteService.GetLocation(model.LocationId);
            if (ModelState.IsValid) {
                site.Abbr = model.Abbr;
                site.Name = model.Name;
                location.Address1 = model.Address1;
                location.Address2 = model.Address2;
                location.City = model.City;
                location.Province = model.Province;
                location.Postal_Code = model.Postal_Code;
                location.Country = model.Country;


                int locationResult = await _siteService.EditLocation(location);
                if (locationResult == 1) {
                    int siteResult = await _siteService.Edit(site);
                    if (siteResult == 1) {
                        return RedirectToAction("Index");
                    } else {
                        ViewBag.ErrorMessage = "Could not edit site";
                        return View(model);
                    }
                } else {
                    ViewBag.ErrorMessage = "Could not edit location";
                    return View(model);
                }
            }
            var countries = Country.GetCountries();
            ViewBag.CountrySelect = new SelectList(countries, "Code", "Name", countries.Where(x => x.Name == site.Location.Country).FirstOrDefault().Code);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FormCollection collection) {
            try {
                string idString = collection.Get("siteId");
                int Id = Convert.ToInt32(idString);
                Site site = _siteService.Get(Id);
                int result = await _siteService.Delete(site);
                if (result > 0) {
                    return RedirectToAction("Index");
                } else {
                    ViewBag.ErrorMessage = "Could not delete site";
                    return RedirectToAction("Index");
                }
            } catch {
                return RedirectToAction("Index");
            }
        }
    }
}