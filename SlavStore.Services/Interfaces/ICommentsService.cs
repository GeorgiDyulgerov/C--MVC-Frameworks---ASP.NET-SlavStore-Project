using SlavStore.Models;

namespace SlavStore.Services
{
    public interface ICommentsService
    {
        void Create(Comment comment, string userId);
        void Delete(int id);
        void Edit(Comment comment);
    }
}