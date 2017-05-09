using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlavStore.Data.Interfaces;
using SlavStore.Models;

namespace SlavStore.Data.Repositories
{
    public class StoreRepository:Repository<Store>
    {
        public StoreRepository(IDbContext context) : base(context)
        {
        }
    }
}
