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
    public class CandidatesResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CandidatesResults
        public async Task<ActionResult> Index(string accesskey)
        {

            
            CandidatesResults.accounts = await db.Accounts.Where(d=>d.key==accesskey).ToListAsync();
            CandidatesResults.results = await db.CandidatesResults.Where(d => d.accesskey == accesskey).ToListAsync();
            CandidatesResults.contents = await db.Contents.ToListAsync();
            CandidatesResults.classes = await db.AttClasses.ToListAsync();
            CandidatesResults.scoredetails.Clear();
            for (int j = 1; j < 5; j++)
            {
                var score = CandidatesResults.results.Where(c => c.classid == j).Sum(s => s.value);
                var color = "Red";
                if(score > 17 )
                {
                    if (score < 24)
                    {
                        color = "Yellow";
                    }
                        
                }
                if (score > 23)
                {
                    color = "Green";
                }

                CandidatesResults.scoredetails.Add(new ScoreDetails
                {
                    accesskey = accesskey,
                    classid = j,
                    score = score,
                    scorelevel = CandidatesResults.classes.Where(d => d.Score == score).Select(s=>s.ScoreLevel).FirstOrDefault(),
                    scoredetail= CandidatesResults.classes.Where(d => d.Score == score).Select(s => s.Text).FirstOrDefault(),
                    title= CandidatesResults.results.Where(c => c.classid == j).Select(s=>s.Title).FirstOrDefault(),
                    color= color,
                });

            }

            TempData["returnUrl"] = Request.UrlReferrer.ToString();

            //return View(await db.CandidatesResults.Where(d => d.accesskey == accesskey).ToListAsync());
            return View(CandidatesResults.results);
        }

        // GET: CandidatesResults/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidatesResults candidatesResults = await db.CandidatesResults.FindAsync(id);
            if (candidatesResults == null)
            {
                return HttpNotFound();
            }
            return View(candidatesResults);
        }

        // GET: CandidatesResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CandidatesResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "accesskey,attribute,value,classid,ClassCategoryID,email")] CandidatesResults candidatesResults)
        {
            if (ModelState.IsValid)
            {
                db.CandidatesResults.Add(candidatesResults);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(candidatesResults);
        }

        // GET: CandidatesResults/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidatesResults candidatesResults = await db.CandidatesResults.FindAsync(id);
            if (candidatesResults == null)
            {
                return HttpNotFound();
            }
            return View(candidatesResults);
        }

        // POST: CandidatesResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "accesskey,attribute,value,classid,ClassCategoryID,email")] CandidatesResults candidatesResults)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidatesResults).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(candidatesResults);
        }

        // GET: CandidatesResults/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidatesResults candidatesResults = await db.CandidatesResults.FindAsync(id);
            if (candidatesResults == null)
            {
                return HttpNotFound();
            }
            return View(candidatesResults);
        }

        // POST: CandidatesResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CandidatesResults candidatesResults = await db.CandidatesResults.FindAsync(id);
            db.CandidatesResults.Remove(candidatesResults);
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
