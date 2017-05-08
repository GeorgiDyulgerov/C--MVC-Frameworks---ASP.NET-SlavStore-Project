using System.Collections.Generic;

namespace SlavStore.Models.ViewModels
{
    public class CreateItemViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsNew { get; set; }

        public int Quantity { get; set; }

        public string Video { get; set; }

        public int Category { get; set; }

        public  List<Category> Categories { get; set; }

        public string Image { get; set; }
    }
}