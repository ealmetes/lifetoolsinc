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
    public class PromotersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promoters
        public async Task<ActionResult> Index()
        {
            return View(await db.Promoters.ToListAsync());
        }

        // GET: Promoters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promoters promoters = await db.Promoters.FindAsync(id);
            if (promoters == null)
            {
                return HttpNotFound();
            }
            return View(promoters);
        }

        // GET: Promoters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promoters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,promoter,discount,code,active,startdate,expiredate")] Promoters promoters)
        {
            if (ModelState.IsValid)
            {
                db.Promoters.Add(promoters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(promoters);
        }

        // GET: Promoters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promoters promoters = await db.Promoters.FindAsync(id);
            if (promoters == null)
            {
                return HttpNotFound();
            }
            return View(promoters);
        }

        // POST: Promoters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,promoter,discount,code,active,startdate,expiredate")] Promoters promoters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promoters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(promoters);
        }

        // GET: Promoters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promoters promoters = await db.Promoters.FindAsync(id);
            if (promoters == null)
            {
                return HttpNotFound();
            }
            return View(promoters);
        }

        // POST: Promoters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Promoters promoters = await db.Promoters.FindAsync(id);
            db.Promoters.Remove(promoters);
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
