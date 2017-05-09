using SlavStore.Models;

namespace SlavStore.Services.Interfaces
{
    public interface ICategoriesService
    {
        void Create(Category category);
        void Delete(int categoryId);
        void Edit(Category category);
    }
}