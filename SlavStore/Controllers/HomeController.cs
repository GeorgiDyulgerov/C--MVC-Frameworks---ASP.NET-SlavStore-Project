using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SlavStore.Data;
using SlavStore.Models;
using SlavStore.Models.ViewModels;

namespace SlavStore.Controllers
{
    public class HomeController : Controller
    {
        private SlavStoreDbContext db = new SlavStoreDbContext();

        public ActionResult Index()
        {
            
            List<Item> items = db.Items.Where(item=>item.Quantity>0).Take(6).OrderByDescending(item=>item.DateAdded).ToList();

            List<HomeViewModel> model = Mapper.Map<List<Item>,List<HomeViewModel>>(items);

            return View(model);
        }
    }
}