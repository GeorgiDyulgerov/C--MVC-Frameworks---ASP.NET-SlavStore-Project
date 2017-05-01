using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            this.Buyers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsNew { get; set; }

        [Range(0,int.MaxValue,ErrorMessage = "Must be positive number")]
        public int Quantity { get; set; }

        public string Video { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual string Image { get; set; }
        
        public virtual Store Seller { get; set; }
        
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ApplicationUser> Buyers { get; set; }

        

    }
}