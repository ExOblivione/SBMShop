using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopProt2.Models;

namespace WebshopProt2.Controllers
{
    public class WebshopController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: Webshop
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        //Browse category
        public ActionResult Browse(string category)
        {
            // Retrieve Genre and its Associated Items from database
            var categoryModel = db.Categories.Include("Items")
                .Single(g => g.CategoryName == category);

            return View(categoryModel);

        }

        // Details
        public ActionResult Details(int id)
        {
            var item = db.Items.Find(id);

            return View(item);
        }

        // get category menu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = db.Categories.ToList();

            return PartialView(categories);
        }

    }
}