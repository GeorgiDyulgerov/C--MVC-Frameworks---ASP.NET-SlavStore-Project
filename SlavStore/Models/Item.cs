using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavStore.Models
{
    public class Item
    {
        //TODO: Add Anotations

        public Item()
        {
            this.Comments = new HashSet<Comment>();
            this.Images = new HashSet<Image>();
            this.Buyers = new HashSet<ApplicationUser>();

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsNew { get; set; }

        public int Quantity { get; set; }

        public string Video { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual Store Seller { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ApplicationUser> Buyers { get; set; }

        

    }
}