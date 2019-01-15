using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebshopProt2.Models;
using WebshopProt2.Configuration;
using System.Threading.Tasks;
using WebshopProt2.Models.WebShop;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestSharp;
using RestSharp.Authenticators;

namespace WebshopProt2.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        AppConfigurations appConfig = new AppConfigurations();

        public List<String> CreditCardTypes { get { return appConfig.CreditCardType; } }

        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            ViewBag.CreditCardTypes = CreditCardTypes;
            var previousOrder = db.Orders.FirstOrDefault(x => x.Username == User.Identity.Name);

            if (previousOrder != null)
                return View(previousOrder);
            else
                return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            ViewBag.CreditCardTypes = CreditCardTypes;
            string result = values[9];
            var order = new Order();
            TryUpdateModel(order);
            order.CreditCard = result;
            try
            {
                order.Username = User.Identity.Name;
                order.Email = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                var currentUserId = User.Identity.GetUserId();
                if (order.SaveInfo && !order.Username.Equals("guest@guest.com"))
                {
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                    var ctx = store.Context;
                    var currentUser = manager.FindById(User.Identity.GetUserId());

                    currentUser.Address = order.Address;
                    currentUser.City = order.City;
                    currentUser.Country = order.Country;
                    currentUser.Phone = order.Phone;
                    currentUser.PostalCode = order.PostalCode;
                    currentUser.FirstName = order.FirstName;

                    await ctx.SaveChangesAsync();
                    await db.SaveChangesAsync();
                }
                //Save Order
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                order = cart.CreateOrder(order);
                
                CheckoutController.SendOrderMessage(order.FirstName, "New Order: " + order.OrderId, order.ToString(order), appConfig.OrderEmail);

                return RedirectToAction("Complete", new { id = order.OrderId });

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }


        private static RestResponse SendOrderMessage(String toName, String subject, String body, String destination)
        {
            RestClient client = new RestClient();
            
            AppConfigurations appConfig = new AppConfigurations();
            //client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              appConfig.EmailApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                appConfig.DomainForApiKey, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", appConfig.FromName + " <" + appConfig.FromEmail + ">");
            request.AddParameter("to", toName + " <" + destination + ">");
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            IRestResponse executor = client.Execute(request);
            return executor as RestResponse;
        }
    }

}