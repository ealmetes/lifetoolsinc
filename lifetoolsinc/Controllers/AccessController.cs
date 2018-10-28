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
  
    public class AccessController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        // POST: Access/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "key,email")] Access model)
        {

            var result = await db.Accounts.Where(e => e.email == model.email).Where(k => k.key == model.key).ToListAsync();
   
            switch (result.Count)
            {
                case 0:
                    ModelState.AddModelError("", "Invalid login attempt.: " + model.email + "|" + model.key);
                    break;                
                case 1:
                    return RedirectToAction("readfirst", "Assessment",new { accesskey =model.key});                 
                default:
                    return View();


            }

            return View();
        }

        // GET: Access/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Access/Delete/5
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
