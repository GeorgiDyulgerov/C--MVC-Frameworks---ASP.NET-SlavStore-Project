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
