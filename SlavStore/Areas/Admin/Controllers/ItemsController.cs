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
    public class ItemsController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();
        private IItemsService service;

        public ItemsController(IItemsService service)
        {
            this.service = service;
        }

        
        public ActionResult List()
        {
            return View(service.GetAllItems());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}