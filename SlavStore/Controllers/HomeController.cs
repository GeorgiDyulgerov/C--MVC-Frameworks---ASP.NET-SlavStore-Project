using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SlavStore.Models;
using SlavStore.Models.ViewModels;

namespace SlavStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            
            List<Item> items = db.Items.Take(6).OrderByDescending(item=>item.DateAdded).ToList();

            List<HomeViewModel> model = Mapper.Map<List<Item>,List<HomeViewModel>>(items);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}