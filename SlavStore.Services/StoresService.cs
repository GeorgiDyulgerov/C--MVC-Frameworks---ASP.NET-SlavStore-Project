using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SlavStore.Data.Interfaces;
using SlavStore.Models;

namespace SlavStore.Services
{
    public class StoresService : Service, IStoresService
    {
        public StoresService(IDbContext context) : base(context)
        {
        }

        public List<Store> GetStores()
        {
            return this.Stores.GetAll().ToList();
        }

        public bool IsStoreNull(int? id)
        {
            Store store = this.Stores.GetFirstOrNull(s => s.Id == id);

            return store == null;
        }


        public Store GetStore(int? id)
        {
            return this.Stores.GetFirstOrNull(s=>s.Id==id);
        }

        public bool CurrentUserHasStore(string currentUserId)
        {
            ApplicationUser user = this.Users.GetFirstOrNull(u => u.Id == currentUserId);

            return user.MyStore != null;
        }

        public void Create(Store store, string currentUserId)
        {
            ApplicationUser user = this.Users.GetFirstOrNull(u => u.Id == currentUserId);
            store.Owner = user;
            this.Stores.Insert(store);
        }

        public bool IsCurrentUserStore(int? id, string currentUserId)
        {
            Store store = this.Stores.GetFirstOrNull(s => s.Id == id);
            ApplicationUser user = this.Users.GetFirstOrNull(u => u.Id == currentUserId);

            return store.Owner == user;
        }

        public void Edit(Store store)
        {
            this.Stores.Update(store);
        }

        public void Delete(int id)
        {
            Store store = this.Stores.GetFirstOrNull(s=>s.Id==id);
            this.Stores.Delete(store);
        }

    }
}
