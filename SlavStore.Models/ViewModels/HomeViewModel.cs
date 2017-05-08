using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavStore.Models.ViewModels
{
    public class HomeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        
    }
}