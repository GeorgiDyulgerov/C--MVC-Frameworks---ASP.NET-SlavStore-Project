using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavStore.Models
{
    public class Comment
    {
        //TODO: Add Anotations

        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int Stars { get; set; }

        public virtual ApplicationUser User{ get; set; }

        public virtual Item Item { get; set; }
    }
}