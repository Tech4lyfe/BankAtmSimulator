using System.Linq;
using System.Web.Mvc;
using BankATMSimulator.Models.BankAtmEntities;
using BankATMSimulator.Models.DbContext;
using BankATMSimulator.Services;

namespace BankATMSimulator.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        
        private ApplicationDbContext _db = new ApplicationDbContext();

    
        [HttpGet]
        public ActionResult Deposit(int? checkingAccountId)
        {


            return View();
        }

        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                
                _db.Transactions.Add(transaction);
                _db.SaveChanges();
                var service = new CheckingAccountService(_db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult TransferFunds(int? checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult TransferFunds(TransferViewModel transfer)
        {
            var mycheckingAccount = _db.CheckingAccounts.Find(transfer.CheckingAccountId);
            if (mycheckingAccount.Balance < transfer.Amount)
            {
                ModelState.AddModelError("Amount", "You have insfficient funds!");
            }
            var targetCheckingAccount =
                _db.CheckingAccounts.FirstOrDefault(model => model.AccountNumber == transfer.TargetCheckingAccountNumber);
            
            if (targetCheckingAccount == null)
            {
                ModelState.AddModelError("TargetAccountNumber", "Invalid Target");
            }
            if (ModelState.IsValid)
            {
                _db.Transactions.Add(new Transaction{Amount = -transfer.Amount,CheckingAccountId = transfer.CheckingAccountId});
                _db.Transactions.Add(new Transaction{Amount = transfer.Amount, CheckingAccountId = targetCheckingAccount.CheckingAccountId});
                _db.SaveChanges();

              
                var service = new CheckingAccountService(_db);
                service.UpdateBalance(transfer.CheckingAccountId);
                service.UpdateBalance(targetCheckingAccount.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            
            
            return View();
        }
       

      
        public void QuickCash( int? checkingAccountId)
        {
            var checkingAccount = _db.CheckingAccounts.Find(checkingAccountId);
            var transaction = new Transaction
            {
                Amount = -50.00,
                CheckingAccount = checkingAccount
            };

            _db.Transactions.Add(transaction);
            _db.SaveChanges();


            var service = new CheckingAccountService(_db);
            service.UpdateBalance(transaction.CheckingAccountId);


        }


        [HttpGet]
        public ActionResult Withdrawal(int? checkingaccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdrawal(Transaction transaction)
        {
            var checkingAccount = _db.CheckingAccounts.Find(transaction.CheckingAccountId);
            if (checkingAccount.Balance < transaction.Amount)
            {
                ModelState.AddModelError("Amount", "You have insfficient funds!");
            }
            else if (ModelState.IsValid)
            {
                transaction.Amount = -transaction.Amount;
                _db.Transactions.Add(transaction);
                _db.SaveChanges();

                var service = new CheckingAccountService(_db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

    }
}
