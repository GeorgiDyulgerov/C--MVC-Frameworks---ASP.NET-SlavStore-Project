using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SlavStore.Data;
using SlavStore.Models;
using SlavStore.Services;
using SlavStore.Utillities.Notifications;

namespace SlavStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CommentsController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();
        private ICommentsService service;

        public CommentsController(ICommentsService service)
        {
            this.service = service;
        }

        // GET: Comments
        
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: Comments/Details/5
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
                this.AddNotification("Successfuly Edited Comment " + comment.Title, NotificationType.SUCCESS);
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
    }
}