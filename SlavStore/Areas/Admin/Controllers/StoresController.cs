using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavStore.Data;
using SlavStore.Services;

namespace SlavStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoresController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();
        private IStoresService service;

        public StoresController(IStoresService service)
        {
            this.service = service;
        }

        // GET: Stores
        
        public ActionResult Index()
        {
            return View(service.GetStores());
        }

    }
}