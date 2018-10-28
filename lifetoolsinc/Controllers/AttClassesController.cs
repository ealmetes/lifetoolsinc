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
    public class AttClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AttClasses
        public async Task<ActionResult> Index()
        {
            return View(await db.AttClasses.ToListAsync());
        }

        // GET: AttClasses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttClasses attClasses = await db.AttClasses.FindAsync(id);
            if (attClasses == null)
            {
                return HttpNotFound();
            }
            return View(attClasses);
        }

        // GET: AttClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Text,ClassCategoryID,Score,ScoreLevel,Title")] AttClasses attClasses)
        {
            if (ModelState.IsValid)
            {
                db.AttClasses.Add(attClasses);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(attClasses);
        }

        // GET: AttClasses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttClasses attClasses = await db.AttClasses.FindAsync(id);
            if (attClasses == null)
            {
                return HttpNotFound();
            }
            return View(attClasses);
        }

        // POST: AttClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Text,ClassCategoryID,Score,ScoreLevel,Title")] AttClasses attClasses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attClasses).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attClasses);
        }

        // GET: AttClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttClasses attClasses = await db.AttClasses.FindAsync(id);
            if (attClasses == null)
            {
                return HttpNotFound();
            }
            return View(attClasses);
        }

        // POST: AttClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AttClasses attClasses = await db.AttClasses.FindAsync(id);
            db.AttClasses.Remove(attClasses);
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
