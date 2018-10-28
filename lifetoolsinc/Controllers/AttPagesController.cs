using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lifetoolsinc.Models;

namespace lifetoolsinc.Controllers
{
    public class AttPagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AttPages
        public async Task<ActionResult> Index()
        {
            return View(await db.AttPages.ToListAsync());
        }

        // GET: AttPages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttPages attPages = await db.AttPages.FindAsync(id);
            if (attPages == null)
            {
                return HttpNotFound();
            }
            return View(attPages);
        }

        // GET: AttPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,page,active,muted")] AttPages attPages)
        {
            if (ModelState.IsValid)
            {
                db.AttPages.Add(attPages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(attPages);
        }

        // GET: AttPages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttPages attPages = await db.AttPages.FindAsync(id);
            if (attPages == null)
            {
                return HttpNotFound();
            }
            return View(attPages);
        }

        // POST: AttPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,page,active,muted")] AttPages attPages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attPages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attPages);
        }

        // GET: AttPages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttPages attPages = await db.AttPages.FindAsync(id);
            if (attPages == null)
            {
                return HttpNotFound();
            }
            return View(attPages);
        }

        // POST: AttPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AttPages attPages = await db.AttPages.FindAsync(id);
            db.AttPages.Remove(attPages);
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
