using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SlavStore.Models
{
    public class Comment
    {
        //TODO: Add Anotations
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Text { get; set; }

        [Required]
        [Range(0,5)]
        public int Stars { get; set; }

        public virtual ApplicationUser User{ get; set; }

        public virtual Item Item { get; set; }
    }
}