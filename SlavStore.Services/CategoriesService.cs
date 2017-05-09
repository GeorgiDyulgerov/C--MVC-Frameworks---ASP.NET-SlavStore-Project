using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SlavStore.Models;
using SlavStore.Services.Interfaces;

namespace SlavStore.Services
{
    public class CategoriesService : Service, ICategoriesService
    {
        public void Create(Category category)
        {
            this.Context.Categories.Add(category);
            this.Context.SaveChanges();
        }

        public void Edit(Category category)
        {
            this.Context.Entry(category).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
           return this.Context.Categories.ToList();
        }

        public void Delete(int categoryId)
        {
            Category category = this.Context.Categories.Find(categoryId);
            this.Context.Categories.Remove(category);
            this.Context.SaveChanges();
        }
    }
}
