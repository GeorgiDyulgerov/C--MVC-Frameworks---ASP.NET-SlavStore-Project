using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SlavStore.Models;

namespace SlavStore.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }

    //    public ApplicationUser()
    //    {
    //        this.ItemsBought = new HashSet<Item>();
    //    }

    //    public string FullName { get; set; }

    //    public string Address { get; set; }

    //    public virtual Store MyStore { get; set; }

    //   public virtual ICollection<Item> ItemsBought { get; set; }


    //}

    public class SlavStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public SlavStoreDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SlavStoreDbContext Create()
        {
            return new SlavStoreDbContext();
        }

        public System.Data.Entity.DbSet<SlavStore.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<SlavStore.Models.Store> Stores { get; set; }

        public System.Data.Entity.DbSet<SlavStore.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<SlavStore.Models.Comment> Comments { get; set; }
    }
}