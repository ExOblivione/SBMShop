using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebshopProt2.Models;
using WebshopProt2.Models.WebShop;

namespace WebshopProt2.Controllers
{
    public class ContactUSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContactUS
        public async Task<ActionResult> Index()
        {
            return View(await db.ContactUS.ToListAsync());
        }

        // GET: ContactUS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUS contactUS = await db.ContactUS.FindAsync(id);
            if (contactUS == null)
            {
                return HttpNotFound();
            }
            return View(contactUS);
        }

        // GET: ContactUS/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContactID,Email,Message,DateofMessage")] ContactUS contactUS)
        {
            if (ModelState.IsValid)
            {
                db.ContactUS.Add(contactUS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contactUS);
        }

        // GET: ContactUS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUS contactUS = await db.ContactUS.FindAsync(id);
            if (contactUS == null)
            {
                return HttpNotFound();
            }
            return View(contactUS);
        }

        // POST: ContactUS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContactID,Email,Message,DateofMessage")] ContactUS contactUS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactUS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contactUS);
        }

        // GET: ContactUS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUS contactUS = await db.ContactUS.FindAsync(id);
            if (contactUS == null)
            {
                return HttpNotFound();
            }
            return View(contactUS);
        }

        // POST: ContactUS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContactUS contactUS = await db.ContactUS.FindAsync(id);
            db.ContactUS.Remove(contactUS);
            await db.SaveChangesAsync();
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
