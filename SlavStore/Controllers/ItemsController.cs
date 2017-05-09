using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SlavStore.Data;
using SlavStore.Utillities.Notifications;
using SlavStore.Models;
using SlavStore.Models.BindingModels;
using SlavStore.Models.ViewModels;
using SlavStore.Services;

namespace SlavStore.Controllers
{
    public class ItemsController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();
        private IItemsService service;

        public ItemsController(IItemsService service)
        {
            this.service = service;
        }

        // GET: Item
        public ActionResult Index(int? page, int? categoryId, string search)
        {

            List<Item> items = service.GetItems();
            if (search != null)
            {
                items = service.SearchItems(items, search);
            }
            if (categoryId != null)
            {
                items = service.GetItemsByCategory(items, categoryId);
            }

            var onePageItems = service.GetPagedList(items, page);
            ViewBag.OnePageOfItems = onePageItems;
            return View();
        }

        // GET: Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (service.IsItemNull(id))
            {
                return HttpNotFound();
            }

            return View(service.GetItem(id));
        }

        // GET: Item/Create
        [Authorize]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();

            if (service.HasStore(userId))
            {
                this.AddNotification("You need to create store first", NotificationType.WARNING);
                return RedirectToAction("Create", "Stores");
            }

            CreateItemViewModel model = service.FillCreateItemViewModel(userId);

            return View(model);
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateItemBindingModel model)
        {
            var userId = User.Identity.GetUserId();

            if (service.HasStore(userId))
            {
                this.AddNotification("You need to create store first", NotificationType.WARNING); this.AddNotification("You need to create store first", NotificationType.ERROR);
                return RedirectToAction("Create", "Stores");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    service.Create(model, userId);
                    this.AddNotification("Item " + model.Name + " successfully created.", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
            }
            CreateItemViewModel vm = service.FillCreateItemViewModel(userId, model);

            return View(vm);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (service.IsItemNull(id))
            {
                return HttpNotFound();
            }
            if (!service.IsCurrentUserStore(userId, id) && !User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index");
            }

            var model = service.FillEditItemViewModel(id);

            return View(model);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditItemBindingModel model)
        {

            var userId = User.Identity.GetUserId();
            if (!service.IsCurrentUserStore(userId, model.Id) && !User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Store store = db.Stores.FirstOrDefault(s => s.Owner.Id == userId);
                Category category = db.Categories.FirstOrDefault(c => c.Name == model.Category);
                Item item = AutoMapper.Mapper.Map<EditItemBindingModel, Item>(model);

                item.Category = category;
                item.Seller = store;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Successfully edited " + model.Name, NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            var vm = service.FillEditItemViewModel(model);

            return View(vm);
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
        {
            var userId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (service.IsItemNull(id))
            {
                return HttpNotFound();
            }
            if (!service.IsCurrentUserStore(userId,id) && !User.IsInRole("Administrator"))
            {
                this.AddNotification("You havo NO PERMITION", NotificationType.ERROR);
                return RedirectToAction("Index");

            }

            return View(service.GetItem(id));
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userId = User.Identity.GetUserId();
            if (!service.IsCurrentUserStore(userId,id) && !User.IsInRole("Administrator"))
            {
                this.AddNotification("You havo NO PERMITION", NotificationType.ERROR);
                return RedirectToAction("Index");
            }
            service.Delete(id);

            this.AddNotification("Successfully Deleted!", NotificationType.WARNING);
            return RedirectToAction("Index");
        }

        // POST: Item/Buy/5
        [Authorize]
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Buy(int id)
        {
            var userId = User.Identity.GetUserId();
            if (service.IsItemNull(id))
            {
                return HttpNotFound();
            }
            service.Buy(id, userId);

            this.AddNotification("Congratulations you successfuly bought the item.", NotificationType.SUCCESS);
            return RedirectToAction("MyItems");
        }

        // GET: Item/MyItems
        [Authorize]
        public ActionResult MyItems()
        {
            var userId = User.Identity.GetUserId();

            return View(service.GetMyItems(userId));
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
