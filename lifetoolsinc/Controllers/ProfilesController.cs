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
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profiles
        public async Task<ActionResult> Index()
        {
            return View(await db.Profiles.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = await db.Profiles.FindAsync(id);
            Accounts.attributes = await db.Accounts.Where(d => d.businessid == profiles.Id).ToListAsync();
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }
        public ActionResult ShoppingCart()
        {
            Checkout checkout = new Checkout();

            return View(checkout);
        }
        public ActionResult Checkout()
        {
            int count = 1;
            string promocode = Request.Form["promocode"];
            Checkout checkout = new Checkout();
            List<Promoters> promoters = db.Promoters.Where(d => d.code == promocode).ToList();
            List<MasterPrices> prices = db.MasterPrices.Where(d => d.type == "Personal").ToList();
            checkout.amount = prices[0].codeprice;
            checkout.totalamount = checkout.amount;

            if (promoters.Count > 0)
            {
                decimal? p = prices[0].codeprice * promoters[0].discount;
                p = p / 100;
                checkout.promocodeid = promoters[0].id;
                checkout.totaldiscount = p;
                checkout.discount = promoters[0].discount;
                Decimal? disc = checkout.totaldiscount * count;


                checkout.amount = checkout.amount - disc;
                checkout.totalamount = checkout.amount - checkout.totaldiscount;
            }
            checkout.amount = checkout.amount * count;
            checkout.keycount = count;


            return View(checkout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pay()
        {
            Accounts accounts = new Accounts();
            Decimal unit = Decimal.Parse(Request.Form["unit"]);
            string promoid = Request.Form["promoid"];
            int count = 1;
            if (ModelState.IsValid)
            {
                for (int i = 0; i < count; ++i)
                {
                    var userid = User.Identity.GetUserId();
                    Profiles profile = await db.Profiles.FindAsync(userid);
                    accounts.key = Helper.PassCode().ToUpper();
                    accounts.statusid = 1;
                    accounts.businessid = userid;
                    accounts.datecreated = DateTime.Now;
                    accounts.email = User.Identity.Name;
                    accounts.candidate = profile.FirstName + " " + profile.LastName;
                    accounts.dateassigned = DateTime.Now;
                    accounts.unit = unit;
                    accounts.type = "Personal";
                    accounts.promocodeid = int.Parse(promoid);

                    db.Accounts.Add(accounts);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("readfirst" ,"Assessment", new { accesskey = accounts.key });
            }

            return View("Index");
        }


        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Zipcode,Email,PhoneNumber,UserName,AccountType,RoleId,Name")] Profiles profiles)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profiles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(profiles);
        }

        // GET: Profiles/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = await db.Profiles.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Zipcode,Email,PhoneNumber,UserName,AccountType,RoleId,Name")] Profiles profiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profiles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(profiles);
        }

        // GET: Profiles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = await db.Profiles.FindAsync(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Profiles profiles = await db.Profiles.FindAsync(id);
            db.Profiles.Remove(profiles);
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
