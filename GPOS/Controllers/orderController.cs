using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using GPOS.Models;
using GPOSDAL;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace GPOS.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "admin")]
    public class orderController : Controller
    {
        // GET: order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new orderModel().Read().ToDataSourceResult(request));
        }

        public ActionResult order_items_order(int oid, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(new order_itemsModel().Read().Where(o => o.oid == oid).ToDataSourceResult(request));
        }
    }
}