using System.Collections.Generic;

namespace SlavStore.Models
{
    public class Category
    {

        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }
}