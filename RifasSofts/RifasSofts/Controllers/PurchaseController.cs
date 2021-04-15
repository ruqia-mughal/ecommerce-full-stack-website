using RifasSofts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;


namespace RifasSofts.Controllers
{
    public class PurchaseController : Controller
    {
        Model1 db = new Model1();

        public ActionResult AddtoCart(int id)
        {

            List<Prod> list = new List<Prod>();
            if (Session["myCart"] == null)
            {
                list = new List<Prod>();
            }
            else
            {
                list = (List<Prod>)Session["myCart"];
            }
            list.Add(db.Prods.Where(p => p.PROD_ID == id).FirstOrDefault());
            list[list.Count - 1].PRO_QUANTITY = 1;
            Session["myCart"] = list;

            return RedirectToAction("Cart");
        }
        public ActionResult MinusFromCart(int RowNo)
        {

            List<Prod> list = (List<Prod>)Session["myCart"];

            list[RowNo].PRO_QUANTITY--;
         

            Session["myCart"] = list;

            return RedirectToAction("Cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {

            List<Prod> list = (List<Prod>)Session["myCart"];

            list[RowNo].PRO_QUANTITY++;

            list[RowNo].PRO_QUANTITY++;
            list[RowNo].PRO_QUANTITY++;
            Session["myCart"] = list;

            return RedirectToAction("Cart");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {


            List<Prod> list = (List<Prod>)Session["myCart"];

            list.RemoveAt(RowNo);

            Session["myCart"] = list;

            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {

            return View();
        }
        public ActionResult CheckoutCustomer()
        {
            return View();
        }
        public ActionResult WishlistCustomer()
        {
            return View();
        }
        public ActionResult Purchase(int? id)
        {
            shopModel s = new shopModel();
            //show model has two lists in int
            // one is cat and product pro
            s.cat = db.Categories.ToList();

            if (id == null)
            {
                s.pro = db.Prods.ToList();
            }
            else
            {
                s.pro = db.Prods.Where(p => p.CATEGORY_FID == id).ToList();
            }

            return View(s);
        }
        public ActionResult ShopDetailCustomer()
        {
            return View();
        }
        public ActionResult PurchaseNow(order o)
        {
            o.ORDER_DATE = System.DateTime.Now;
            o.ORDER_STATUS = "Paid";
            o.ORDER_TYPE = "Purchase";
            Session["Order"] = o;

            return RedirectToAction("OrderBooked");
        }


        public ActionResult OrderBooked()

        {
            order o = (order)Session["Order"];

            db.orders.Add(o);
            db.SaveChanges();
            List<Prod> p = (List<Prod>)Session["myCart"];

            for (int i = 0; i < p.Count; i++)
            {
                orderDetail od = new orderDetail();

                int orderId = db.orders.Max(x => x.ORDER_ID);

                od.ORDER_FID = orderId;

                od.PRODUCT_FID = p[i].PROD_ID;
                od.QUANTITY = p[i].PRO_QUANTITY;
                od.PURCHASE_PRICE = p[i].PROD_PURCHASE_PRICE;
                od.SALE_PRICE = p[i].PROD_SALE_PRICE;
                db.orderDetails.Add(od);
                db.SaveChanges();

            }

            return View();
        }
     
    }
}