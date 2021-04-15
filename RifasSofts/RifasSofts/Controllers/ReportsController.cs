using RifasSofts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RifasSofts.Controllers
{
    public class ReportsController : Controller
    {
        Model1 db = new Model1();

        
        public ActionResult PurchaseReport()
        {
            var o = db.orders.Where(x=>x.ORDER_TYPE=="Purchase").ToList();

            return View(o);

        }
        public ActionResult SaleReport()
        {
            var o = db.orders.Where(x => x.ORDER_TYPE == "Sale").ToList();

            return View(o);

        }
        public ActionResult Invoice( int id)
        {

            var o = db.orders.Where(x => x.ORDER_ID == id).ToList();
            return View(o);

        }
    }
}