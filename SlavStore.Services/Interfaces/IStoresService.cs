using System.Collections.Generic;
using SlavStore.Models;

namespace SlavStore.Services
{
    public interface IStoresService
    {
        void Create(Store store, string currentUserId);
        bool CurrentUserHasStore(string currentUserId);
        void Delete(int id);
        void Edit(Store store);
        Store GetStore(int? id);
        List<Store> GetStores();
        bool IsCurrentUserStore(int? id, string currentUserId);
        bool IsStoreNull(int? id);
    }
}