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
using SlavStore.Helpers;
using SlavStore.Models;
using SlavStore.Models.BindingModels;
using SlavStore.Models.ViewModels;

namespace SlavStore.Controllers
{
    public class ItemsController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();

        // GET: Item
        public ActionResult Index(int? page,int? categoryId,string search)
        {

            List<Item> items = db.Items.Where(item=>item.Quantity>0).OrderByDescending(item => item.DateAdded).ToList();
            if (search != null )
            {
                items = items.FindAll(item => item.Name.ToLower().Contains(search.ToLower()) || item.Description.ToLower().Contains(search.ToLower()) || item.Category.Name.ToLower().Contains(search.ToLower()));
            }
            if (categoryId != null)
            {
                items = items.FindAll(item => item.Category.Id == categoryId);
            }
            List<HomeViewModel> model = Mapper.Map<List<Item>, List<HomeViewModel>>(items);

            var pageNumber = page ?? 1;
            var onePageItems = model.ToPagedList(pageNumber, 9);
            ViewBag.OnePageOfItems = onePageItems;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult List()
        {
            return View(db.Items.ToList());
        }

        // GET: Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Item/Create
        [Authorize]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            Store store = db.Stores.FirstOrDefault(s => s.Owner.Id == userId);

            if (store == null)
            {
                this.AddNotification("You need to create store first", NotificationType.WARNING);
                return RedirectToAction("Create", "Stores");
            }

            CreateItemViewModel model = new CreateItemViewModel();
            List<Category> categories = db.Categories.ToList();
            model.Categories = categories;

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
            Store store = db.Stores.FirstOrDefault(s => s.Owner.Id == userId);

            if (store == null)
            {
                this.AddNotification("You need to create store first", NotificationType.WARNING); this.AddNotification("You need to create store first", NotificationType.ERROR);
                return RedirectToAction("Create", "Stores");
            }
            else
            {
                Category category = db.Categories.FirstOrDefault(c => c.Id == model.Category);
                Item item = Mapper.Map<CreateItemBindingModel, Item>(model);
                item.DateAdded = DateTime.Now;
                item.Seller = store;
                item.Category = category;
                if (ModelState.IsValid)
                {
                    db.Items.Add(item);
                    db.SaveChanges();
                    this.AddNotification("Item " + item.Name +" successfully created.", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
            }
            CreateItemViewModel vm = Mapper.Map<CreateItemBindingModel, CreateItemViewModel>(model);
            List<Category> categories = db.Categories.ToList();
            vm.Categories = categories;
            return View(vm);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            Store store = db.Stores.FirstOrDefault(s => s.Owner.Id == userId);

            if (item.Seller != store)
            {
                return RedirectToAction("Index");

            }
            EditItemViewModel model = Mapper.Map<Item, EditItemViewModel>(item);
            model.Category = item.Category.Name;
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
            Store store = db.Stores.FirstOrDefault(s => s.Owner.Id == userId);
            Category category = db.Categories.FirstOrDefault(c => c.Name == model.Category);

            Item item = Mapper.Map<EditItemBindingModel, Item>(model);

            item.Category = category;
            item.Seller = store;

            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Successfully edited" +item.Name, NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            EditItemViewModel vm = Mapper.Map<EditItemBindingModel, EditItemViewModel>(model);
            return View(vm);
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            Store store = db.Stores.FirstOrDefault(s => s.Owner.Id == userId);

            if (item.Seller != store)
            {
                this.AddNotification("You havo NO PERMITION", NotificationType.ERROR);
                return RedirectToAction("Index");

            }
            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);

            var userId = User.Identity.GetUserId();
            Store store = db.Stores.FirstOrDefault(s => s.Owner.Id == userId);

            if (item.Seller != store)
            {
                this.AddNotification("You havo NO PERMITION", NotificationType.ERROR);
                return RedirectToAction("Index");

            }
            db.Items.Remove(item);
            db.SaveChanges();
            this.AddNotification("Successfully Deleted " + item.Name, NotificationType.WARNING);
            return RedirectToAction("Index");
        }

        // POST: Item/Buy/5
        [Authorize]
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Buy(int id)
        {

            Item item = db.Items.Find(id);

            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == userId);

            user.ItemsBought.Add(item);
            db.Entry(user).State = EntityState.Modified;
            item.Quantity -= 1;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            this.AddNotification("Congratulations you successfuly bought " + item.Name, NotificationType.SUCCESS);
            return RedirectToAction("MyItems");
        }

        // GET: Item/MyItems
        [Authorize]
        public ActionResult MyItems()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == userId);
            List<Item> items = user.ItemsBought.ToList();

            return View(items);
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
