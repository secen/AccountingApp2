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
    public class CryptoOrdersController : Controller
    {
        private AccountingDBEntities1 db = new AccountingDBEntities1();

        // GET: CryptoOrders
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var cryptoOrders = db.CryptoOrders.Where(x => x.UserID == userId);
            var cryptoOrdersList = cryptoOrders.ToList();
            var Totals = new List<double>();
            foreach(CryptoOrder cryptoOrder in cryptoOrdersList)
            {
                var crypto = db.Cryptoes.Find(cryptoOrder.CryptoID);
                Totals.Add(crypto.Value * cryptoOrder.Value);
            }
            ViewBag.Totals = Totals;
            return View(cryptoOrdersList);
        }

        // GET: CryptoOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CryptoOrder cryptoOrder = db.CryptoOrders.Find(id);
            if (cryptoOrder == null)
            {
                return HttpNotFound();
            }
            return View(cryptoOrder);
        }

        // GET: CryptoOrders/Create
        public ActionResult Create()
        {
            ViewBag.CryptoID = new SelectList(db.Cryptoes, "ID", "Name");
            return View();
        }

        // POST: CryptoOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Value,CryptoID")] CryptoOrder cryptoOrder)
        {
            if (ModelState.IsValid)
            {
                cryptoOrder.UserID = User.Identity.GetUserId();
                db.CryptoOrders.Add(cryptoOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CryptoID = new SelectList(db.Cryptoes, "ID", "Name", cryptoOrder.CryptoID);
            return View(cryptoOrder);
        }

        // GET: CryptoOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CryptoOrder cryptoOrder = db.CryptoOrders.Find(id);
            if (cryptoOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CryptoID = new SelectList(db.Cryptoes, "ID", "Name", cryptoOrder.CryptoID);
            return View(cryptoOrder);
        }

        // POST: CryptoOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Value,UserID,CryptoID")] CryptoOrder cryptoOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cryptoOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CryptoID = new SelectList(db.Cryptoes, "ID", "Name", cryptoOrder.CryptoID);
            return View(cryptoOrder);
        }

        // GET: CryptoOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CryptoOrder cryptoOrder = db.CryptoOrders.Find(id);
            if (cryptoOrder == null)
            {
                return HttpNotFound();
            }
            return View(cryptoOrder);
        }

        // POST: CryptoOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CryptoOrder cryptoOrder = db.CryptoOrders.Find(id);
            db.CryptoOrders.Remove(cryptoOrder);
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
