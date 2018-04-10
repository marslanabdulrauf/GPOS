using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPOS.Models;
using GPOSDAL;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace GPOS.Controllers
{
    public class customerController : Controller
    {
        // GET: customer
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, item product)
        {
            if (product != null && ModelState.IsValid)
            {
                product.bid = 1;
                product.lastsold = DateTime.Now;
                product.tag = true;
                new itemModel().Create(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new customerModel().Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, item product)
        {
            if (product != null && ModelState.IsValid)
            {
                new itemModel().Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, item product)
        {
            if (product != null)
            {
                new itemModel().Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));

        }
        public JsonResult Filter_Read(string text)
        {
            text = text.ToLower();
            var products = new itemModel().Read();
            if (!string.IsNullOrEmpty(text))
            {
                products = products.Where(i => (i.name.ToLower().Contains(text) || i.Barcode.Equals(text)) && i.isActive && i.tag);
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}