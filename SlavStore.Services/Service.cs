using SlavStore.Data;
using SlavStore.Data.Interfaces;
using SlavStore.Data.Repositories;

namespace SlavStore.Services
{
    public abstract class Service
    {
        public Service(IDbContext context)
        {
            this.Items=new ItemRepository(context);
            this.Categories =new CategoryRepository(context);
            this.Comments = new CommentRepository(context);
            this.Stores = new StoreRepository(context);
            this.Home = new HomeRepository(context);
            this.Users=new UserRepository(context);
        }

        public ItemRepository Items { get; }
        public CategoryRepository Categories { get; }
        public StoreRepository Stores { get; }
        public CommentRepository Comments { get; }
        public HomeRepository Home { get; }
        public UserRepository Users { get; }

    }
}
