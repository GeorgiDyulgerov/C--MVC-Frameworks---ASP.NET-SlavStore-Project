using System.Linq;
using System.Web.Mvc;
using SlavStore.Data;
using SlavStore.Services.Interfaces;

namespace SlavStore.Controllers
{

    public class CategoriesListController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();
        private ICategoriesService service;

        public CategoriesListController(ICategoriesService service)
        {
            this.service = service;
        }

        public ActionResult CategoriesList()
        {
            return PartialView(service.GetCategories());
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
