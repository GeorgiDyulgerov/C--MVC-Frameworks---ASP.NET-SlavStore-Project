using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavStore.Models.BindingModels
{
    public class CreateItemBindingModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsNew { get; set; }

        public int Quantity { get; set; }

        public string Video { get; set; }

        public int Category { get; set; }
    }
}