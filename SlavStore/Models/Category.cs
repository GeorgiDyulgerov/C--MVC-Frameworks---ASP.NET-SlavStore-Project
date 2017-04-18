using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SlavStore.Models
{
    public class Category
    {

        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }
}