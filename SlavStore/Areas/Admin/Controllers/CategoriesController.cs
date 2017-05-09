using System.Linq;
using System.Net;
using System.Web.Mvc;
using SlavStore.Data;
using SlavStore.Models;
using SlavStore.Services.Interfaces;
using SlavStore.Utillities.Notifications;

namespace SlavStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        private SlavStoreDbContext _slavStoreDb = new SlavStoreDbContext();
        private ICategoriesService service;

        public CategoriesController(ICategoriesService service)
        {
            this.service = service;
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(service.GetCategories());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _slavStoreDb.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                service.Create(category);
                this.AddNotification("Created Category " + category.Name, NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _slavStoreDb.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                service.Edit(category);
                this.AddNotification("Successfuly Edited " + category.Name, NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _slavStoreDb.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            this.AddNotification("Successfuly Deleted!", NotificationType.WARNING);
            return RedirectToAction("Index");
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
