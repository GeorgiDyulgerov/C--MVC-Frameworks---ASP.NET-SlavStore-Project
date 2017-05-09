using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SlavStore.Data.Interfaces;
using SlavStore.Models;
using SlavStore.Services.Interfaces;

namespace SlavStore.Services
{
    public class CategoriesService : Service, ICategoriesService
    {

        public CategoriesService(IDbContext context) : base(context)
        {
        }

        public void Create(Category category)
        {
            this.Categories.Insert(category);
        }

        public void Edit(Category category)
        {
            this.Categories.Update(category);
        }

        public List<Category> GetCategories()
        {
           return this.Categories.GetAll().ToList();
        }

        public void Delete(int categoryId)
        {
            Category category = this.Categories.GetFirstOrNull(c=>c.Id==categoryId);
            this.Categories.Delete(category);
        }

    }
}
