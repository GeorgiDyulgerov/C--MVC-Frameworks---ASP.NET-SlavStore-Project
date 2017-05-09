using System.Linq;
using System.Web.Mvc;
using SlavStore.Data;
using SlavStore.Services.Interfaces;

namespace SlavStore.Controllers
{

    public class CategoriesListController : Controller
    {
        private SlavStoreDbContext _slavStoreDb = new SlavStoreDbContext();
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
                _slavStoreDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
