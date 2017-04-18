using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SlavStore.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public virtual Item Item { get; set; }
    }
}