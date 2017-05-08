using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavStore.Models.ViewModels
{
    public class EditItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsNew { get; set; }

        public int Quantity { get; set; }

        public string Video { get; set; }

        public string Category { get; set; }

        public DateTime DateAdded { get; set; }

        public string Image { get; set; }
    }
}