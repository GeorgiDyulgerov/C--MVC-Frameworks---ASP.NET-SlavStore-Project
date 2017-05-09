using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using SlavStore.Models;
using SlavStore.Models.BindingModels;
using SlavStore.Models.ViewModels;

namespace SlavStore.Services
{
    public class ItemsService : Service, IItemsService 
    {
        public List<Item> GetItems()
        {
            return this.Context.Items.Where(item=>item.Quantity>0).OrderByDescending(item => item.DateAdded).ToList();
        }

        public List<Item> SearchItems(List<Item> items,string search)
        {
           return items.FindAll(item => item.Name.ToLower().Contains(search.ToLower()) || item.Description.ToLower().Contains(search.ToLower()) || item.Category.Name.ToLower().Contains(search.ToLower()));            
        }

        public void Create(CreateItemBindingModel model, string userId)
        {
            Store store = this.Context.Stores.FirstOrDefault(s => s.Owner.Id == userId);
            Category category = this.Context.Categories.FirstOrDefault(c => c.Id == model.Category);
            Item item = AutoMapper.Mapper.Map<CreateItemBindingModel, Item>(model);
            item.DateAdded = DateTime.Now;
            item.Seller = store;
            item.Category = category;
            this.Context.Items.Add(item);
            this.Context.SaveChanges();
        }

        public bool IsCurrentUserStore(string userId, int? id)
        {
            Store store = this.Context.Stores.FirstOrDefault(s => s.Owner.Id == userId);

            Item item = this.Context.Items.Find(id);

            return store == item.Seller;
        }

        public bool IsItemNull(int? id)
        {
            Item item = this.Context.Items.Find(id);

            return item == null;
        }

        public EditItemViewModel FillEditItemViewModel(int? id)
        {
            Item item = this.Context.Items.Find(id);
            EditItemViewModel model = AutoMapper.Mapper.Map<Item, EditItemViewModel>(item);
            model.Category = item.Category.Name;

            return model;
        }

        public EditItemViewModel FillEditItemViewModel(EditItemBindingModel model)
        {
            EditItemViewModel vm = AutoMapper.Mapper.Map<EditItemBindingModel, EditItemViewModel>(model);

            return vm;
        }

        public Item Edit(EditItemBindingModel model, string userId)
        {
            Store store = this.Context.Stores.FirstOrDefault(s => s.Owner.Id == userId);
            Category category = this.Context.Categories.FirstOrDefault(c => c.Name == model.Category);
            Item item = AutoMapper.Mapper.Map<EditItemBindingModel, Item>(model);

            item.Category = category;
            item.Seller = store;

            return item;
        }

        public Item GetItem(int? id)
        {
            return this.Context.Items.Find(id);
        }

        public void Delete(int id)
        {
            Item item = this.Context.Items.Find(id);
            foreach (var comment in item.Comments.ToList())
            {
                this.Context.Comments.Remove(comment);
            }
            this.Context.Items.Remove(item);
            this.Context.SaveChanges();
        }

        public void Buy(int id, string userId)
        {
            Item item = this.Context.Items.Find(id);
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Id == userId);
            user.ItemsBought.Add(item);
            this.Context.Entry(user).State = EntityState.Modified;
            item.Quantity -= 1;
            this.Context.Entry(item).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public List<Item> GetMyItems(string userId)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Id == userId);
            List<Item> items = user.ItemsBought.ToList();

            return items;
        }

        public List<Item> GetAllItems()
        {
            return this.Context.Items.ToList();
        }

        public List<Item> GetItemsByCategory(List<Item> items, int? categoryId)
        {
            return items.FindAll(item => item.Category.Id == categoryId);
        }

        public IPagedList GetPagedList(List<Item> items, int? page)
        {
            List<HomeViewModel> model = AutoMapper.Mapper.Map<List<Item>, List<HomeViewModel>>(items);

            var pageNumber = page ?? 1;
            var onePageItems = model.ToPagedList(pageNumber, 9);
            return onePageItems;
        }

        public bool HasStore(string userId)
        {
            Store store = this.Context.Stores.FirstOrDefault(s => s.Owner.Id == userId);

            return store == null;
        }


        public CreateItemViewModel FillCreateItemViewModel(string userId)
        {
            List<Category> categories = this.Context.Categories.ToList();

            return new CreateItemViewModel() {Categories = categories};
        }

        public CreateItemViewModel FillCreateItemViewModel(string userId, CreateItemBindingModel model)
        {
            CreateItemViewModel vm = AutoMapper.Mapper.Map<CreateItemBindingModel, CreateItemViewModel>(model);
            List<Category> categories = this.Context.Categories.ToList();
            vm.Categories = categories;

            return new CreateItemViewModel() { Categories = categories };
        }
    }
}
