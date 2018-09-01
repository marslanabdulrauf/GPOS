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
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, customer product)
        {
            if (product != null && ModelState.IsValid)
            {
                product.bid = 1;
                product.tag = true;
                new customerModel().Create(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new customerModel().Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, customer product)
        {
            if (product != null && ModelState.IsValid)
            {
                new customerModel().Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, customer product)
        {
            if (product != null)
            {
                new customerModel().Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));

        }
        public JsonResult Filter_Read(string text)
        {
            text = text.ToLower();
            var products = new customerModel().Read();
            if (!string.IsNullOrEmpty(text))
            {
                products = products.Where(i =>i.name != null && i.name.ToLower().Contains(text));
                products = products.Where(i => i.isActive);
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}