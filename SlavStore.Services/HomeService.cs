using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using SlavStore.Data;
using SlavStore.Data.Interfaces;
using SlavStore.Models;
using SlavStore.Models.ViewModels;

namespace SlavStore.Services
{
    public class HomeService : Service, IHomeService
    {
        public HomeService(IDbContext context) : base(context)
        {
            
        }

        public List<HomeViewModel> GetHomeItems()
        {
            List<Item> items = this.Items.GetAll().Where(item => item.Quantity > 0).Take(6).OrderByDescending(item => item.DateAdded).ToList();

            List<HomeViewModel>  model = AutoMapper.Mapper.Map<List<Item>, List<HomeViewModel>>(items);

            return model;
        }
    }
}
