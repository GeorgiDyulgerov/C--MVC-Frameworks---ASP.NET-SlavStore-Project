﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SlavStore.Models
{
    public class Store
    {
        public Store()
        {
            this.ItemsForSale = new HashSet<Item>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public  virtual  ApplicationUser Owner { get; set; }
       
        public virtual ICollection<Item> ItemsForSale { get; set; }

    }
}