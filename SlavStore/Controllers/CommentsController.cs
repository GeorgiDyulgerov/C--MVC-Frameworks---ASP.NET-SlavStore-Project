using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using SlavStore.Data;
using SlavStore.Utillities.Notifications;
using SlavStore.Models;
using SlavStore.Services;

namespace SlavStore.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();
        private ICommentsService service;

        public CommentsController(ICommentsService service)
        {
            this.service = service;
        }

        // GET: Comments
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: Comments/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Stars,Item")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                service.Create(comment,userId);
                this.AddNotification("Successfuly Commented", NotificationType.SUCCESS);
                return RedirectToAction("Details","Items", new { id = comment.Item.Id });
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Stars")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                service.Edit(comment);
                this.AddNotification("Successfuly Edited Comment "+comment.Title, NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.Delete(id);
            this.AddNotification("Successfuly Deleted!", NotificationType.WARNING);
            return RedirectToAction("Index");
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
