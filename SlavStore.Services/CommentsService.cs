using System.Data.Entity;
using System.Linq;
using SlavStore.Models;
using Microsoft.AspNet.Identity;

namespace SlavStore.Services
{
    public class CommentsService : Service, ICommentsService
    {
        public void Create(Comment comment,string userId)
        {
            Item item = this.Context.Items.FirstOrDefault(i => i.Id == comment.Item.Id);
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Id == userId);
            comment.User = user;
            comment.Item = item;
            this.Context.Comments.Add(comment);
            this.Context.SaveChanges();
        }

        public void Edit(Comment comment)
        {
            this.Context.Entry(comment).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Comment comment = this.Context.Comments.Find(id);
            this.Context.Comments.Remove(comment);
            this.Context.SaveChanges();
        }
    }
}
