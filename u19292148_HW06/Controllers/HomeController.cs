using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u19292148_HW06.Models;
using PagedList;

namespace u19292148_HW06.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        public ActionResult Index()
        {
            
            //var sales = db.order_items;
            //var sales1 = db.order_items.Include(p => p.product).Include(p=>p.order).Where(p => p.product.category_id == 6);
            //int[] data = new int[]
            List<int> data = new List<int>
            {
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 1).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 2).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 3).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 4).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 5).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 6).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 7).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 8).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 9).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 10).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 11).ToList().Sum(p => p.quantity),
                db.order_items.Where(p => p.product.category_id == 6 && p.order.order_date.Month == 12).ToList().Sum(p => p.quantity),
            };
            ViewBag.Message = data.ToArray();

            return View();
        }

    }
}