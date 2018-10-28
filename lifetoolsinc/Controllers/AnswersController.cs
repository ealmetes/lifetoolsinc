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
    public class AnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answers
        public async Task<ActionResult> Index(string accesskey)
        {
            return View(await db.Answers.Where(d=>d.accesskey==accesskey).ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "accesskey,id,attribute,value,classid,email,datetaken,orguid,pageid")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(answers);
        }

        // GET: Answers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "accesskey,id,attribute,value,classid,email,datetaken,orguid,pageid")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(answers);
        }

        // GET: Answers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Answers answers = await db.Answers.FindAsync(id);
            db.Answers.Remove(answers);
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
