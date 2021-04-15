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
    public class HomeController : Controller
    {
        Model1 db = new Model1();
       



        public ActionResult IndexCustomer()
        {
            return View();
        }
        public ActionResult MyAccount()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult tableAdmin()
        {
            return View();
        }
        public ActionResult chartsAdmin()
        {
            return View();
        }
        public ActionResult formAdmin()
        {
            return View();

        }
        public ActionResult tabPanelAdmin()
        {
            return View();
        }

        public ActionResult uiAdmin()
        {
            return View();
        }

        public ActionResult AboutCustomer()
        {
            return View();
        }
        public ActionResult AddtoCart(int id)
        {
            //session global state (list,bloean,float,kisi b kism ki info rakh skty) that can be accessed anywhere
            //cart ,payment,order biling

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
            list[list.Count-1].PRO_QUANTITY = 1;
            Session["myCart"] = list;

            return RedirectToAction("CartCustomer");
        }
        public ActionResult MinusFromCart(int RowNo)
        {
           
            List<Prod> list = (List<Prod>)Session["myCart"];
          
            list[RowNo].PRO_QUANTITY --;
            Session["myCart"] = list;

            return RedirectToAction("CartCustomer");
        }
        public ActionResult PlusToCart(int RowNo)
        {

            List<Prod> list = (List<Prod>)Session["myCart"];

            list[RowNo].PRO_QUANTITY++;
            Session["myCart"] = list;

            return RedirectToAction("CartCustomer");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {


            List<Prod> list = (List<Prod>)Session["myCart"];

            list.RemoveAt(RowNo);

            Session["myCart"] = list;

            return RedirectToAction("CartCustomer");
        }
        public ActionResult CartCustomer()
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
        public ActionResult ShopCustomer(int ? id)
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
                s.pro = db.Prods.Where(p=>p.CATEGORY_FID==id).ToList();
            }
           
            return View(s);
        }
        public ActionResult ShopDetailCustomer()
        {
            return View();
        }
        public ActionResult PayNow(order o)
        {
            o.ORDER_DATE = System.DateTime.Now;
            o.ORDER_STATUS = "Paid";
            o.ORDER_TYPE = "Sale";
            Session["Order"] = o;

            return Redirect("OrderBooked");
        }
        public ActionResult ContactCustomer()
        {
            ViewBag.Message = "Your contact Customer page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult OrderBooked()

        {
            order o = (order)Session["Order"];


           MailMessage mail = new MailMessage();
            mail.From = new MailAddress("popy6065@gmail.com");
            mail.To.Add(o.ORDER_EMAIL);
            mail.Subject = "Order Confirmation";
            mail.Body = "<b>Rifsa_Softs!</b><br/> thanks for your order  of "+o.ORDER_NAME+ "at"+System.DateTime.Now+"we will deliver within 24 hours ";
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
         
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("popy6065@gmail.com", "popyruqia..");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            

            String api = "https://lifetimesms.com/json?api_token=8dffbc6d9cda49784b5f9bfd09fcc2e64c6da05177&api_secret=RifsaSofts&to=" + o.ORDER_CONTACT + "&from=RifsaSofts&message=Order Confirmation.Thanks.....Your Order Has been booked";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);

            var httpResponse = (HttpWebResponse)req.GetResponse();
//     saving data
            db.orders.Add(o);
            db.SaveChanges();
            List<Prod> p = (List<Prod>)Session["myCart"];
            for (int i=0;i<p.Count; i++)
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
        [HttpPost]
        public ActionResult Login(Admin a)
        { //please count  records from admin tabe where email and password is matched with our posted email and password
           // if email and password is matched thrn result is 1
           //else if not matched result is 0

            int result = db.Admins.Where(x => x.ADMIN_EMAIL == a.ADMIN_EMAIL && x.ADMIN_PASSWORD == a.ADMIN_PASSWORD).Count();
            if(result == 1)
            {
                return RedirectToAction("IndexAdmin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email and Password";
                return View();
            }
            
        }
    }
}