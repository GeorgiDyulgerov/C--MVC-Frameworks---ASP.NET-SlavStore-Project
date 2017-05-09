using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SlavStore.Data;
using SlavStore.Utillities.Notifications;
using SlavStore.Models;
using SlavStore.Services;

namespace SlavStore.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {
        private SlavStoreDbContext _slavStoreDb = new SlavStoreDbContext();
        private IStoresService service;

        public StoresController(IStoresService service)
        {
            this.service = service;
        }
       
        // GET: Stores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (service.IsStoreNull(id))
            {
                return HttpNotFound();
            }
            return View(service.GetStore(id));
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            string currentUserId = User.Identity.GetUserId();
            if (service.CurrentUserHasStore(currentUserId))
            {
                this.AddNotification("You already have a store", NotificationType.ERROR);
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        // POST: Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Id,Owner")] Store store)
        {
            string currentUserId = User.Identity.GetUserId();

            if (service.CurrentUserHasStore(currentUserId))
            {
                this.AddNotification("You already have a store", NotificationType.ERROR);
                return RedirectToAction("Index");
            }
            if (store.Name!=null)
            {
                service.Create(store, currentUserId);

                this.AddNotification("Store Created", NotificationType.SUCCESS);
                return RedirectToAction("Create","Items");
            }

            return View(store);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int? id)
        {
            string currentUserId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (service.IsStoreNull(id))
            {
                return HttpNotFound();
            }
            if (!service.IsCurrentUserStore(id,currentUserId) && !User.IsInRole("Administrator"))
            {
                this.AddNotification("You have no permition", NotificationType.ERROR);
                return RedirectToAction("Index","Home");
            }

            return View(service.GetStore(id));
        }

        // POST: Stores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Owner")] Store store)
        {
            string currentUserId = User.Identity.GetUserId();
            if (!service.IsCurrentUserStore(store.Id, currentUserId) && !User.IsInRole("Administrator"))
            {
                this.AddNotification("You have no permition", NotificationType.ERROR);
                return RedirectToAction("Index","Home");
            }

            if (ModelState.IsValid)
            {
                _slavStoreDb.Entry(store).State = EntityState.Modified;
                _slavStoreDb.SaveChanges();
                this.AddNotification("Store "+store.Name+" Edited", NotificationType.SUCCESS);
                return RedirectToAction("Details","Stores",new {id=store.Id});
            }
            return View(store);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int? id)
        {
            string currentUserId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (service.IsStoreNull(id))
            {
                return HttpNotFound();
            }
            if (!service.IsCurrentUserStore(id,currentUserId) && !User.IsInRole("Administrator"))
            {
                this.AddNotification("You have no permition", NotificationType.ERROR);
                return RedirectToAction("Index","Home");
            }

            return View(service.GetStore(id));
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            if (!service.IsCurrentUserStore(id, currentUserId) && !User.IsInRole("Administrator"))
            {
                this.AddNotification("You have no permition", NotificationType.ERROR);
                return RedirectToAction("Index","Home");
            }
            service.Delete(id);

            return RedirectToAction("Index","Home");
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
