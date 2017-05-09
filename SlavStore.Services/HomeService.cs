using System.Collections.Generic;
using System.Linq;
using SlavStore.Models;
using SlavStore.Models.ViewModels;

namespace SlavStore.Services
{
    public class HomeService : Service, IHomeService
    {
        public List<HomeViewModel> GetHomeItems()
        {
            List<Item> items = this.Context.Items.Where(item => item.Quantity > 0).Take(6).OrderByDescending(item => item.DateAdded).ToList();

            List<HomeViewModel>  model = AutoMapper.Mapper.Map<List<Item>, List<HomeViewModel>>(items);

            return model;
        }

    }
}
