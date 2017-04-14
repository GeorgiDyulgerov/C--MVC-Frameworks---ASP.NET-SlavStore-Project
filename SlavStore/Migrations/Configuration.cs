using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SlavStore.Models;

namespace SlavStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SlavStore.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SlavStore.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!context.Roles.Any())
            {
                var roleCreatedResult = roleManager.Create(new IdentityRole("Administrator"));

                if (roleCreatedResult.Succeeded)
                {
                    var newUser = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                    var result = userManager.Create(newUser, "Aa123456");
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(newUser.Id, "Administrator");
                    }
                    else
                    {
                        throw new Exception(string.Join(";", result.Errors));
                    }

                }
                else
                {
                    throw new Exception(string.Join(";", roleCreatedResult.Errors));
                }

            }
        }
    }
}
