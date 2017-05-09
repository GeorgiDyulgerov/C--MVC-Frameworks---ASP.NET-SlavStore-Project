using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SlavStore.Models;

namespace SlavStore.Services
{
    public class StoresService : Service, IStoresService
    {
        public List<Store> GetStores()
        {
            return this.Context.Stores.ToList();
        }

        public bool IsStoreNull(int? id)
        {
            Store store = this.Context.Stores.Find(id);

            return store == null;
        }


        public Store GetStore(int? id)
        {
            return this.Context.Stores.Find(id);
        }

        public bool CurrentUserHasStore(string currentUserId)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Id == currentUserId);

            return user.MyStore != null;
        }

        public void Create(Store store, string currentUserId)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Id == currentUserId);
            store.Owner = user;
            this.Context.Stores.Add(store);
            this.Context.SaveChanges();
        }

        public bool IsCurrentUserStore(int? id, string currentUserId)
        {
            Store store = this.Context.Stores.Find(id);
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Id == currentUserId);

            return store.Owner == user;
        }

        public void Edit(Store store)
        {
            this.Context.Entry(store).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Store store = this.Context.Stores.Find(id);
            this.Context.Stores.Remove(store);
            this.Context.SaveChanges();
        }
    }
}
