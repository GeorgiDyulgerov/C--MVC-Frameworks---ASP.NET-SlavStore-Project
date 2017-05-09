using SlavStore.Data;

namespace SlavStore.Services
{
    public abstract class Service
    {
        public Service()
        {
            this.Context = new SlavStoreDbContext();
        }

        protected SlavStoreDbContext Context { get;}
    }
}
