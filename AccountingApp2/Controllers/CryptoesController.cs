using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountingApp2.DBA;

namespace AccountingApp2.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CryptoesController : Controller
    {
        private AccountingDBEntities1 db = new AccountingDBEntities1();

        // GET: Cryptoes
        public ActionResult Index()
        {
            return View(db.Cryptoes.ToList());
        }

        // GET: Cryptoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crypto crypto = db.Cryptoes.Find(id);
            if (crypto == null)
            {
                return HttpNotFound();
            }
            return View(crypto);
        }

        // GET: Cryptoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cryptoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Value")] Crypto crypto)
        {
            if (ModelState.IsValid)
            {
                db.Cryptoes.Add(crypto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crypto);
        }

        // GET: Cryptoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crypto crypto = db.Cryptoes.Find(id);
            if (crypto == null)
            {
                return HttpNotFound();
            }
            return View(crypto);
        }

        // POST: Cryptoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Value")] Crypto crypto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crypto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crypto);
        }

        // GET: Cryptoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crypto crypto = db.Cryptoes.Find(id);
            if (crypto == null)
            {
                return HttpNotFound();
            }
            return View(crypto);
        }

        // POST: Cryptoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crypto crypto = db.Cryptoes.Find(id);
            db.Cryptoes.Remove(crypto);
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
