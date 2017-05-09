using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlavStore.Data.Interfaces;
using SlavStore.Models;

namespace SlavStore.Data.Repositories
{
    public class ItemRepository:Repository<Item>
    {
        public ItemRepository(IDbContext context) : base(context)
        {
        }
    }
}
