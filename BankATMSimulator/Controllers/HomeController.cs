using System.Linq;
using System.Web.Mvc;
using BankATMSimulator.Models.DbContext;
using Microsoft.AspNet.Identity;

namespace BankATMSimulator.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId =
                _db.CheckingAccounts.First(c => c.ApplicationUserId == userId).CheckingAccountId;
            ViewBag.CheckingAccountId = checkingAccountId;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void QuickCash(int? checkingAccountId)
        {
            
        }
    }
}