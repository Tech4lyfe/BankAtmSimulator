using System.Data.Entity.Migrations;
using System.Linq;
using BankATMSimulator.Models;
using BankATMSimulator.Models.DbContext;
using BankATMSimulator.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankATMSimulator.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin@mvcbanking.com"))
            {
                var user = new ApplicationUser {UserName = "admin@mvcbanking.com", Email = "admin@mvcbanking.com"};
                userManager.Create(user, "passW0rd!");

                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("admin@mvcbanking", "admin", 30000, user.Id);

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole {Name = "Admin"});
                context.SaveChanges();

                userManager.AddToRole(user.Id, "Admin");

            }
        }
    }
}
