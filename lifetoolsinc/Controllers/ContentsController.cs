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
    public class ContentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contents
        public async Task<ActionResult> Index()
        {
            return View(await db.Contents.ToListAsync());
        }

        // GET: Contents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = await db.Contents.FindAsync(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // GET: Contents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,text,type")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                db.Contents.Add(contents);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contents);
        }

        // GET: Contents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = await db.Contents.FindAsync(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,text,type")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contents).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contents);
        }

        // GET: Contents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = await db.Contents.FindAsync(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contents contents = await db.Contents.FindAsync(id);
            db.Contents.Remove(contents);
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
