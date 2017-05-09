using System.Collections.Generic;
using PagedList;
using SlavStore.Models;
using SlavStore.Models.BindingModels;
using SlavStore.Models.ViewModels;

namespace SlavStore.Services
{
    public interface IItemsService
    {
        CreateItemViewModel FillCreateItemViewModel(string userId);
        CreateItemViewModel FillCreateItemViewModel(string userId,CreateItemBindingModel model);
        List<Item> GetItems();
        List<Item> GetItemsByCategory(List<Item> items, int? categoryId);
        IPagedList GetPagedList(List<Item> items, int? page);
        bool HasStore(string userId);
        List<Item> SearchItems(List<Item> items, string search);
        void Create(CreateItemBindingModel model, string userId);
        bool IsCurrentUserStore(string userId, int? id);
        bool IsItemNull(int? id);
        EditItemViewModel FillEditItemViewModel(int? id);
        EditItemViewModel FillEditItemViewModel(EditItemBindingModel model);
        Item Edit(EditItemBindingModel model,string userId);
        Item GetItem(int? id);
        void Delete(int id);
        void Buy(int id, string userId);
        List<Item> GetMyItems(string userId);
        List<Item> GetAllItems();
    }
}