using System.Linq;
using BankATMSimulator.Models.BankAtmEntities;
using BankATMSimulator.Models.DbContext;

namespace BankATMSimulator.Services
{
    public class CheckingAccountService
    {
        private ApplicationDbContext db;
        //private IApplicationDbContext db;

        public CheckingAccountService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public void CreateCheckingAccount(string firstName, string lastName, decimal initialBalance, string userId)
        {


            var accountNumber = (123456 + db.CheckingAccounts.Count()).ToString().PadLeft(10, '0');

            var checkingAccount = new CheckingAccount
            {
                FirstName = firstName,
                LastName = lastName,
                AccountNumber = accountNumber,
                Balance = 0,
                ApplicationUserId = userId
            };

            db.CheckingAccounts.Add(checkingAccount);
            db.SaveChanges();
        }

        public void UpdateBalance(int checkingAccountId)
        {
            var checkingAccount = db.CheckingAccounts.First(c => c.CheckingAccountId == checkingAccountId);
            checkingAccount.Balance =
                db.Transactions.Where(c => c.CheckingAccountId == checkingAccountId).Sum(a => a.Amount);
            db.SaveChanges();
        }
    }
}