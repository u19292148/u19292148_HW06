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
    public class order_itemsController : Controller
    {
        int numberElementsPerPage = 10;
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: order_items
        public ActionResult Index(string Search, int? pageIndex)
        {
            var items = db.orders.Include(a => a.order_items);

            if (!string.IsNullOrEmpty(Search))
            {
                DateTime DateToSearch;
                if(DateTime.TryParse(Search, out DateToSearch))
                {
                    items = db.orders.Include(a => a.order_items).Where(a => a.order_date == DateToSearch);
                }
            }
            int pageNumber = (pageIndex ?? 1);
            return View(items.ToList().ToPagedList(pageNumber, numberElementsPerPage));
        }

    }
}
