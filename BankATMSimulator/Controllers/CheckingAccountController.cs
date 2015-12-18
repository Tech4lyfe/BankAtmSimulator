using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BankATMSimulator.Models.BankAtmEntities;
using BankATMSimulator.Models.DbContext;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BankATMSimulator.Controllers
{
    [Authorize]
    public class CheckingAccountController : Controller
    {
       
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: CheckingAccount/Details/5
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccount = _db.CheckingAccounts.First(c => c.ApplicationUserId == userId);

            return View(checkingAccount);
        }

        [Authorize(Roles="Admin")]
        public ActionResult DetialsForAdim(int id)
        {
            var checkingAccount = _db.CheckingAccounts.Find(id);
            return View("Details", checkingAccount);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var checkingAccounts = _db.CheckingAccounts.Find(id);
            if (checkingAccounts == null)
            {
                return HttpNotFound();
            }
            return View(checkingAccounts);
        }

        [HttpPost]
        public ActionResult Edit(CheckingAccount checkingAccount)
        {
            
            if (ModelState.IsValid)
            {
                _db.Entry(checkingAccount).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
           
            return View(checkingAccount);
        }

        [HttpGet]
        public ActionResult DeleteAccounts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var checkingAccounts = _db.CheckingAccounts.Find(id);
            if (checkingAccounts == null)
            {
                return HttpNotFound();
            }
            return View(checkingAccounts);
        }

        [HttpPost]
        public ActionResult DeleteAccounts(int id)
        {
            var checkingAccounts = _db.CheckingAccounts.Find(id);
            _db.CheckingAccounts.Remove(checkingAccounts);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

       
        public ActionResult List(string searchBy, string search, int? page)
        {
            return View(searchBy == "FirstName" ? _db.CheckingAccounts.Where(c => c.FirstName.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1,3) : _db.CheckingAccounts.Where(c => c.AccountNumber == search || search == null).ToList().ToPagedList(page ?? 1,3));
        }

        public ActionResult Statement(int id, int? page)
        {

            var checkingAccount = _db.CheckingAccounts.Find(id);
            return View(checkingAccount.Transactions.ToList().ToPagedList(page ?? 1,10));
        }
        
    }
}
