using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SlavStore.Data.Interfaces;
using SlavStore.Models;

namespace SlavStore.Data
{
    public class SlavStoreDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public SlavStoreDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SlavStoreDbContext Create()
        {
            return new SlavStoreDbContext();
        }

        public DbSet<Item> Items { get; set; }
               
        public DbSet<Store> Stores { get; set; }
               
        public DbSet<Category> Categories { get; set; }
               
        public DbSet<Comment> Comments { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}