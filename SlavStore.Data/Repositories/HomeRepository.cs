using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlavStore.Data.Interfaces;
using SlavStore.Models;

namespace SlavStore.Data.Repositories
{
    public class HomeRepository:Repository<Item>
    {
        public HomeRepository(IDbContext context) : base(context)
        {
        }
    }
}
