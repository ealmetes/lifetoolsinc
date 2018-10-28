using lifetoolsinc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Controllers
{
    public class AssessmentController : Controller
    {
        // POST: Candidate/Create
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> readfirst(string accesskey)
        {
            Accounts accounts = await db.Accounts.FindAsync(accesskey);
            if (accounts == null)
            {
                return RedirectToAction("ShoppingCart", "Profiles");
            }
            if (accounts.type == "Personal")
            {
                var results = await db.CandidatesResults.Where(d => d.accesskey == accesskey).ToListAsync();
                if (results.Count == 32)
                {
                    return RedirectToAction("Index", "CandidatesResults", new { accesskey = accounts.key });
                }
          
            }
            return View(accounts);
            
        }
        public async Task<ActionResult> Finish(string key)
        {
            Accounts accounts = await db.Accounts.FindAsync(key);
            if (accounts.type == "Personal")
            {
                return View(accounts);
            }

            return View();
        }
        // GET: Candidate
        public async Task<ActionResult> Index(string accesskey,string businessid,string email)
        {
            TempData["pageid"] = "0";
            Accounts accounts = await db.Accounts.FindAsync(accesskey);
            int _pageid = 1;
            int pagecount= db.AttPages.Where(m => m.muted == false).Where(a => a.active == true).OrderByDescending(o => o.id).Select(s=>s.id).Take(1).First();
            TempData["pagecount"] = pagecount;
            if (Request.Form["q"] != null)
            {
              
                string[] choices = Request.Form["q"].Split(',');
                string[] picks = Request.Form["a"].Split(',');
                if (picks.Contains("0")==true)
                {
                    return View(accounts);

                }
                for (int i=0; i < choices.Count(); ++i)
                {
                    
                    int aid = int.Parse(choices[i]);
                    int choice = int.Parse(picks[i].Replace("drag",""));
                    Attributes attributes = db.Attributes.Find(aid);
                    Answers answers = new Answers();
                    answers.accesskey = accesskey;
                    answers.attribute = attributes.Text;
                    answers.classid = attributes.ClassCategoryid;
                    answers.pageid = attributes.pageid;
                    answers.orguid = businessid;
                    answers.email = email;
                    answers.value = choice;
                    answers.datetaken = DateTime.Now;
                    _pageid = attributes.pageid;
                    TempData["pageid"] = _pageid;
                    if (db.Answers.Any(p => p.pageid == attributes.pageid && p.value == choice && p.accesskey==accesskey))
                    {
           
                    }
                    else
                    {
                        db.Answers.Add(answers);
                        await db.SaveChangesAsync();
                

                    }
             
                    }
          
                    accounts = await db.Accounts.FindAsync(accesskey);
                    accounts.pages = db.AttPages.Where(m => m.muted == false).Where(a => a.active == true).Where(p => p.id > _pageid).ToList();
                    if(accounts.pages.Count() > 0)
                    {
                    accounts.page = accounts.pages.OrderBy(o => o.id).Select(s => s.page).Take(1).First();
                    accounts.choices = db.Attributes.Where(m => m.pageid == accounts.page).ToList();
                    return View(accounts);
                    }else
                    {
                    Accounts accounts1 = await db.Accounts.FindAsync(accesskey);
                    accounts1.dateexecuted = DateTime.Now;
                    db.Entry(accounts1).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Finish", "Assessment", new { key = accesskey });
                    }
                           
            }else
            {

                var lastpage = 0;
                if(db.Answers.Any(d => d.accesskey == accesskey))
                {
                    lastpage = db.Answers.Where(d => d.accesskey == accesskey).OrderByDescending(o => o.pageid).Select(s => s.pageid).Take(1).First();
                }
             
                if (lastpage > 0)
                {
                    try
                    {
                        accounts.pages = db.AttPages.Where(m => m.muted == false).Where(a => a.active == true).Where(p => p.id == lastpage).ToList();
                        accounts.page = accounts.pages.Select(s => s.page).Take(1).First();
                        accounts.choices = db.Attributes.Where(m => m.pageid == accounts.page).ToList();
                        TempData["pageid"] = accounts.page;
                    }
                    catch
                    {

                    }
                   
                }else
                {
                    try
                    {
                        accounts.pages = db.AttPages.Where(m => m.muted == false).Where(a => a.active == true).ToList();
                        accounts.page = accounts.pages.Select(s => s.page).Take(1).First();
                        accounts.choices = db.Attributes.Where(m => m.pageid == accounts.page).ToList();
                        TempData["pageid"] = accounts.page;
                    }
                    catch
                    {

                    }
                }
          
           
              
                if (accounts.page == pagecount)
                {
                    Accounts accounts1 = await db.Accounts.FindAsync(accesskey);
                    accounts1.dateexecuted = DateTime.Now;
                    db.Entry(accounts1).State = EntityState.Modified;
                    await db.SaveChangesAsync();
              
                    return RedirectToAction("Finish", "Assessment", new { key = accesskey });
                }
                else
                {
                    accounts.page = accounts.pages.OrderBy(o => o.id).Select(s => s.page).Take(1).First();
                    accounts.choices = db.Attributes.Where(m => m.pageid == accounts.page).ToList();
                }
            }


            return View(accounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Answers([Bind(Include = "accesskey,id,attribute,value,classid,email,datetaken,orguid,pageid")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(answers);
        }


        // GET: Candidate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Candidate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidate/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Candidate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Candidate/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Candidate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Candidate/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
