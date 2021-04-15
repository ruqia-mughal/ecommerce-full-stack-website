using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RifasSofts.Models;

namespace RifasSofts.Controllers
{
    public class ProdsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Prods
        public ActionResult Index()
        {
            var prods = db.Prods.Include(p => p.Category);
            return View(prods.ToList());
        }

        // GET: Prods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod prod = db.Prods.Find(id);
            if (prod == null)
            {
                return HttpNotFound();
            }
            return View(prod);
        }

        // GET: Prods/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORY_FID = new SelectList(db.Categories, "CATEGORY_ID", "CATEGORY_NAME");
            return View();
        }

        // POST: Prods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prod prod)
        {
            if (ModelState.IsValid)
            {
                prod.PRO_PIC.SaveAs(Server.MapPath("~/productPICTURE/"+prod.PRO_PIC.FileName));
                prod.PROD_PICTURE = "~/productPICTURE/"+prod.PRO_PIC.FileName;

                db.Prods.Add(prod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CATEGORY_FID = new SelectList(db.Categories, "CATEGORY_ID", "CATEGORY_NAME", prod.CATEGORY_FID);
            return View(prod);
        }

        // GET: Prods/Edit/5
        public ActionResult Edit(int? id)
        {
           
            Prod prod = db.Prods.Find(id);
            ViewBag.CATEGORY_FID = new SelectList(db.Categories, "CATEGORY_ID", "CATEGORY_NAME", prod.CATEGORY_FID);
            return View(prod);
        }

        // POST: Prods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prod prod)
        {
            if (ModelState.IsValid)
            {
                if(prod.PRO_PIC!= null)
                {

               

                prod.PRO_PIC.SaveAs(Server.MapPath("~/productPICTURE/" + prod.PRO_PIC.FileName));
                prod.PROD_PICTURE = "~/productPICTURE/" + prod.PRO_PIC.FileName;
                }
                db.Entry(prod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORY_FID = new SelectList(db.Categories, "CATEGORY_ID", "CATEGORY_NAME", prod.CATEGORY_FID);
            return View(prod);
        }

        // GET: Prods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod prod = db.Prods.Find(id);
            if (prod == null)
            {
                return HttpNotFound();
            }
            return View(prod);
        }

        // POST: Prods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prod prod = db.Prods.Find(id);
            db.Prods.Remove(prod);
            db.SaveChanges();
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
