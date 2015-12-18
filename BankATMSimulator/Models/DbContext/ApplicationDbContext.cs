using System.Data.Entity;
using BankATMSimulator.Models.BankAtmEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankATMSimulator.Models.DbContext
{

   
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
 
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        public IDbSet<Transaction> Transactions { get; set; }

        //public System.Data.Entity.DbSet<BankATMSimulator.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<BankATMSimulator.Models.ApplicationUser> ApplicationUsers { get; set; }

       

       
    }
}

