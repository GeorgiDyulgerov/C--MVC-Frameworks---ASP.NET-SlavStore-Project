using System.Data.Entity;
using System.Linq;
using SlavStore.Models;
using Microsoft.AspNet.Identity;
using SlavStore.Data.Interfaces;

namespace SlavStore.Services
{
    public class CommentsService : Service, ICommentsService
    {
        public CommentsService(IDbContext context) : base(context)
        {
        }
        public void Create(Comment comment,string userId)
        {
            Item item = this.Items.GetFirstOrNull(i => i.Id == comment.Item.Id);
            ApplicationUser user = this.Users.GetFirst(u => u.Id == userId);
            comment.User = user;
            comment.Item = item;
            this.Comments.Insert(comment);
        }

        public void Edit(Comment comment)
        {
            this.Comments.Update(comment);

        }

        public void Delete(int id)
        {
            Comment comment = this.Comments.GetById(id);
            this.Comments.Delete(comment);
        }


    }
}
