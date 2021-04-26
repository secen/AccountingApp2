using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountingApp2.DBA;
using Microsoft.AspNet.Identity;

namespace AccountingApp2.Controllers
{

    [Authorize(Roles = "User")]
    public class StockOrdersController : Controller
    {
        private AccountingDBEntities1 db = new AccountingDBEntities1();

        // GET: StockOrders
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var stockOrders = db.StockOrders.Where(s => s.UserID == userId);
            var stockOrdersList = stockOrders.ToList();
            var totals = new List<double>();
            foreach (StockOrder order in stockOrders)
            {
                Stock stock = db.Stocks.Find(order.StockID);
                totals.Add(order.Value * stock.Value);
            }
            ViewBag.Totals = totals;
            return View(stockOrders.ToList());
        }

        // GET: StockOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOrder stockOrder = db.StockOrders.Find(id);
            if (stockOrder == null)
            {
                return HttpNotFound();
            }
            return View(stockOrder);
        }

        // GET: StockOrders/Create
        public ActionResult Create()
        {
            ViewBag.StockID = new SelectList(db.Stocks, "id", "Name");
            return View();
        }

        // POST: StockOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UserID,StockID,Value")] StockOrder stockOrder)
        {
            if (ModelState.IsValid)
            {
                stockOrder.UserID = User.Identity.GetUserId();
                db.StockOrders.Add(stockOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockID = new SelectList(db.Stocks, "id", "Name", stockOrder.StockID);
            return View(stockOrder);
        }

        // GET: StockOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOrder stockOrder = db.StockOrders.Find(id);
            if (stockOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockID = new SelectList(db.Stocks, "id", "Name", stockOrder.StockID);
            return View(stockOrder);
        }

        // POST: StockOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UserID,StockID,Value")] StockOrder stockOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockID = new SelectList(db.Stocks, "id", "Name", stockOrder.StockID);
            return View(stockOrder);
        }

        // GET: StockOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOrder stockOrder = db.StockOrders.Find(id);
            if (stockOrder == null)
            {
                return HttpNotFound();
            }
            return View(stockOrder);
        }

        // POST: StockOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockOrder stockOrder = db.StockOrders.Find(id);
            db.StockOrders.Remove(stockOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
