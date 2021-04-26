using AccountingApp2.DBA;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingApp2.Controllers
{
    [Authorize(Roles = "User")]
    public class DashboardController : Controller
    {
        private AccountingDBEntities1 db = new AccountingDBEntities1();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IncomeStatement()
        {
            List<double> pricesList1 = new List<double>();
            List<double> pricesList2 = new List<double>();
            string userId = User.Identity.GetUserId();
            var cryptoOrders = db.CryptoOrders.Where(x => x.UserID == userId);
            var cryptoOrdersList = cryptoOrders.ToList();
            var stockOrders = db.StockOrders.Where(x => x.UserID == userId);
            var stockOrdersList = stockOrders.ToList();
            List<string> cryptoNames = new List<string>();
            List<string> stockNames = new List<string>();
            foreach (CryptoOrder cryptoOrder in cryptoOrdersList)
            {
                var crypto = db.Cryptoes.Find(cryptoOrder.CryptoID);
                pricesList1.Add(crypto.Value * cryptoOrder.Value);
                cryptoNames.Add(crypto.Name);
            }
            foreach (StockOrder cryptoOrder in stockOrdersList)
            {
                var crypto = db.Stocks.Find(cryptoOrder.StockID);
                pricesList2.Add(crypto.Value * cryptoOrder.Value);
                stockNames.Add(crypto.Name);
            }
            ViewBag.Prices1 = pricesList1;
            ViewBag.Prices2 = pricesList2;
            ViewBag.Stocks = stockOrdersList;
            ViewBag.Crypto = cryptoOrdersList;
            ViewBag.CryptoNames = cryptoNames;
            ViewBag.StockNames = stockNames;
            return View();
        }
    }
}