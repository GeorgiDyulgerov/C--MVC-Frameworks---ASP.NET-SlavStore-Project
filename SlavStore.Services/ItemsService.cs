using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using SlavStore.Data.Interfaces;
using SlavStore.Models;
using SlavStore.Models.BindingModels;
using SlavStore.Models.ViewModels;

namespace SlavStore.Services
{
    public class ItemsService : Service, IItemsService 
    {
        public ItemsService(IDbContext context) : base(context)
        {
        }

        public List<Item> GetItems()
        {
            return this.Items.GetAll().Where(item=>item.Quantity>0).OrderByDescending(item => item.DateAdded).ToList();
        }

        public List<Item> SearchItems(List<Item> items,string search)
        {
           return items.FindAll(item => item.Name.ToLower().Contains(search.ToLower()) || item.Description.ToLower().Contains(search.ToLower()) || item.Category.Name.ToLower().Contains(search.ToLower()));            
        }

        public void Create(CreateItemBindingModel model, string userId)
        {
            Store store = this.Stores.GetFirstOrNull(s => s.Owner.Id == userId);
            Category category = this.Categories.GetFirstOrNull(c => c.Id == model.Category);
            Item item = AutoMapper.Mapper.Map<CreateItemBindingModel, Item>(model);
            item.DateAdded = DateTime.Now;
            item.Seller = store;
            item.Category = category;
            this.Items.Insert(item);
        }

        public bool IsCurrentUserStore(string userId, int? id)
        {
            Store store = this.Stores.GetFirstOrNull(s => s.Owner.Id == userId);

            Item item = this.Items.GetById(id);

            return store == item.Seller;
        }

        public bool IsItemNull(int? id)
        {
            Item item = this.Items.GetById(id);

            return item == null;
        }

        public EditItemViewModel FillEditItemViewModel(int? id)
        {
            Item item = this.Items.GetById(id);
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
            Store store = this.Stores.GetFirstOrNull(s => s.Owner.Id == userId);
            Category category = this.Categories.GetFirst(c => c.Name == model.Category);
            Item item = AutoMapper.Mapper.Map<EditItemBindingModel, Item>(model);

            item.Category = category;
            item.Seller = store;

            return item;
        }

        public Item GetItem(int? id)
        {
            return this.Items.GetById(id);
        }

        public void Delete(int id)
        {
            Item item = this.Items.GetById(id);
            foreach (var comment in item.Comments.ToList())
            {
                this.Comments.Delete(comment);
            }
            this.Items.Delete(item);
        }

        public void Buy(int id, string userId)
        {
            Item item = this.Items.GetById(id);
            ApplicationUser user = this.Users.GetFirstOrNull(u => u.Id == userId);
            user.ItemsBought.Add(item);
            this.Users.Update(user);
            item.Quantity -= 1;
            this.Items.Update(item);
        }

        public List<Item> GetMyItems(string userId)
        {
            ApplicationUser user = this.Users.GetFirstOrNull(u => u.Id == userId);
            List<Item> items = user.ItemsBought.ToList();

            return items;
        }

        public List<Item> GetAllItems()
        {
            return this.Items.GetAll().ToList();
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
            Store store = this.Stores.GetFirstOrNull(s => s.Owner.Id == userId);

            return store == null;
        }


        public CreateItemViewModel FillCreateItemViewModel(string userId)
        {
            List<Category> categories = this.Categories.GetAll().ToList();

            return new CreateItemViewModel() {Categories = categories};
        }

        public CreateItemViewModel FillCreateItemViewModel(string userId, CreateItemBindingModel model)
        {
            CreateItemViewModel vm = AutoMapper.Mapper.Map<CreateItemBindingModel, CreateItemViewModel>(model);
            List<Category> categories = this.Categories.GetAll().ToList();
            vm.Categories = categories;

            return new CreateItemViewModel() { Categories = categories };
        }

  
    }
}
