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
    public class ProductViewModel
    {

        public int product_id { get; set; }
        public string product_name { get; set; }
        public short model_year { get; set; }

        public decimal list_price { get; set; }

        public string category_name { get; set; }
        public string brand_name { get; set; }
        public int? quantity { get; set; }

        public List<stock> stocks { get; set; }


    }


    public class productsController : Controller
    {
        int numberElementsPerPage = 10;
        private BikeStoresEntities db = new BikeStoresEntities();


        public ActionResult Index(int pageIndex = 1)
        {

            return View(db.products.OrderBy(p => p.product_id).ToPagedList(pageIndex, numberElementsPerPage));
        }

        // GET: products
        public JsonResult GetProductList(string Search = "", int? pageIndex = 1 )
        {
            int skip = (int)pageIndex * 10;
            int take = 10;

            //db.Configuration.ProxyCreationEnabled = false;
            var products = db.products
                .Where(p => p.product_name.Contains(Search))
                .OrderBy(p => p.product_id)
                .Skip(skip)
                .Take(take)
                .Include(p => p.brand)
                .Include(p => p.category)
                .Include(o => o.stocks)
                .Select(x =>new ProductViewModel
            { 
                brand_name = x.brand.brand_name,
                category_name = x.category.category_name,
                list_price = x.list_price,
                model_year = x.model_year,
                product_id = x.product_id,
                product_name = x.product_name

            }).ToList();

            //if (!string.IsNullOrEmpty(Search))
            //{
            //    products = products.Where(p => p.product_name.Contains(Search)).ToList();
            //}

            return Json(products,JsonRequestBehavior.AllowGet);
           // return Json(products.ToList().ToPagedList(pageNumber, numberElementsPerPage), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Add(product products)
        {
           
            db.products.Add(products);
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProduct(product product1)
        {
            db.products.Add(product1);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult GetbyID(int ID)
        {
            var product = db.products
                .Include(p => p.brand)
                .Include(p => p.category)
                .Select(x => new ProductViewModel
            {
                brand_name = x.brand.brand_name,
                category_name = x.category.category_name,
                list_price = x.list_price,
                model_year = x.model_year,
                product_id = x.product_id,
                product_name = x.product_name
            }).FirstOrDefault(x => x.product_id == ID);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID2(int ID)
        {
            var product = db.products
                .Include(p => p.brand)
                .Include(p => p.category)
                .Include(p => p.stocks)
                .Select(x => new ProductViewModel
                {
                    brand_name = x.brand.brand_name,
                    category_name = x.category.category_name,
                    list_price = x.list_price,
                    model_year = x.model_year,
                    product_id = x.product_id,
                    product_name = x.product_name
                }).FirstOrDefault(x => x.product_id == ID);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(product products)
        {
            var data = db.products.FirstOrDefault(x => x.product_id == products.product_id);
            if (data != null)
            {
                data.product_name = products.product_name;
                data.model_year = products.model_year;
                data.list_price = products.list_price;
                data.brand = products.brand;
                data.category = products.category;
                db.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            var data = db.products.FirstOrDefault(x => x.product_id == ID);
            db.products.Remove(data);
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        //GET: products/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    product product = db.products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// GET: products/Create
        //public ActionResult Create()
        //{
        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
        //    return View();
        //}

        //// POST: products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
        //    return View(product);
        //}

        //// GET: products/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    product product = db.products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
        //    return View(product);
        //}

        //// POST: products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
        //    ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
        //    return View(product);
        //}

        //// GET: products/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    product product = db.products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// POST: products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    product product = db.products.Find(id);
        //    db.products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
