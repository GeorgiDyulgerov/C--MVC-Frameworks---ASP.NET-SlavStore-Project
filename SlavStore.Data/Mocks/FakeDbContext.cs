using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlavStore.Data.Interfaces;
using SlavStore.Models;

namespace SlavStore.Data.Mocks
{
    public class FakeDbContext : IDbContext
    {
        public FakeDbContext()
        {
            this.Items = new FakeItemSet();
            this.Categories = new FakeCategorySet();
            this.Comments = new FakeCommentSet();
            this.Stores = new FakeStoreSet();

        }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Store> Stores { get; set; }


        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return new FakeDbSet<TEntity>();

        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
