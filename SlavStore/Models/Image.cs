using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavStore.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public virtual Item Item { get; set; }
    }
}