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
using Microsoft.AspNet.Identity;


namespace lifetoolsinc.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accounts
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                bool adminRole = User.IsInRole("Admin");                
                bool masterRole = User.IsInRole("MasterUser");
                if (masterRole == true || adminRole==true)
                {
                    return View(await db.Accounts.Where(d=>d.businessid== userid).ToListAsync());
          
                }
            }
       
           
            return RedirectToAction("Index", "Account");
        }

        // GET: Accounts/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = await db.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        
        public ActionResult Checkout()
        {
            int count = int.Parse(Request.Form["demo3"]);
            string promocode= Request.Form["promocode"];
            Checkout checkout = new Checkout();
            List<Promoters> promoters =  db.Promoters.Where(d => d.code == promocode).ToList();
            List<MasterPrices> prices = db.MasterPrices.Where(d => d.type == "Business").ToList();
            checkout.amount = prices[0].codeprice;
            checkout.totalamount = checkout.amount;
            checkout.amount = checkout.amount * count;
            checkout.keycount = count;

            if (promoters.Count > 0)
            {
                if (promoters[0].discount == 100)
                {
                    checkout.promocodeid = promoters[0].id;
                    checkout.totaldiscount = checkout.amount;
                    checkout.discount = promoters[0].discount;
                    checkout.amount = new decimal(0.0);
                    checkout.totalamount = checkout.amount;
                }
                else
                {
                    decimal? p = checkout.amount * promoters[0].discount ;
                    p = p / 100;
                    checkout.promocodeid = promoters[0].id;
                    checkout.totaldiscount = p;
                    checkout.discount = promoters[0].discount;
                    Decimal? disc = checkout.totaldiscount * count;


                    checkout.amount = checkout.amount - p;
                    checkout.totalamount = checkout.amount - checkout.totaldiscount;
                }


            }
     
            

            return View(checkout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pay()
        {
         
            Decimal unit = Decimal.Parse(Request.Form["unit"]);

            string promoid = "";
            if (Request.Form["promoid"] != null)
            {
                promoid = Request.Form["promoid"];
            }

            int count = int.Parse(Request.Form["count"]);
            if (ModelState.IsValid)
            {
                for(int i=0; i<count; ++i)
                {
                    Accounts accounts = new Accounts();
                    var k = Helper.PassCode().ToUpper();
                    accounts.key =k;
                    accounts.statusid = 1;
                    accounts.businessid = User.Identity.GetUserId();
                    accounts.datecreated = DateTime.Now;
                    accounts.unit = unit;
                    accounts.promocodeid = int.Parse(promoid);
                    accounts.type = "Candidate";
                    db.Accounts.Add(accounts);
                    await db.SaveChangesAsync();
                }
    
                return RedirectToAction("Index");
            }
            Accounts accounts1 = new Accounts();
            return View(accounts1);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "key,statusid,candidate,phone,businessid,email,datecreated,dateassigned,dateexecuted,unit,promocodeid")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                accounts.key = "";
                accounts.statusid = 1;
                //accounts.userid = User.Identity.GetUserId();
                accounts.businessid = User.Identity.GetUserId();
                accounts.datecreated = DateTime.Now;
                db.Accounts.Add(accounts);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(accounts);
        }

        // GET: Accounts/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = await db.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "key,statusid,candidate,phone,businessid,email,datecreated,dateassigned,dateexecuted,unit,promocodeid")] Accounts accounts)
        {
            //if (ModelState.IsValid)
            //{
                db.Entry(accounts).State = EntityState.Modified;
                await db.SaveChangesAsync();
                ///Helper.InviteEmail(accounts.email, accounts.email, accounts.businessid, accounts.key);
                return RedirectToAction("Index");
            //}
            //return View(accounts);
        }

        // GET: Accounts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = await db.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Accounts accounts = await db.Accounts.FindAsync(id);
            db.Accounts.Remove(accounts);
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
