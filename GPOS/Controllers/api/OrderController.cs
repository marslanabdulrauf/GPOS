using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GPOS.CrystalReports;
using GPOS.Models;
using GPOSDAL;
using Microsoft.AspNet.Identity;

namespace GPOS.Controllers.api
{
    public class OrderController : ApiController
    {
        GPOSEntities db= new GPOSEntities();

        private static int localorder=1;
        private int sale;
        private int[] ids;
        private String[] names;
        private int[] unit;
        private int[] qnty;
        private int[] total;
        private int totalDisocount = 0;
        private int totalamount = 0;
        private int rcv;
        private int blnc;
        private int ordernumber;

        public IHttpActionResult SetOrder([FromUri] string items, [FromUri] int tot, [FromUri] int dis,
            [FromUri] int rcv)
        {
            String[] list = items.Split(',');
            ids=new int[list.Length];
            names = new String[list.Length];
            unit = new int[list.Length];
            qnty = new int[list.Length];
            total = new int[list.Length];
            sale = tot;
            this.rcv = rcv;
            blnc = rcv - tot;
            for (int i = 0; i < list.Length; i++)
            {
                String[] temp = list[i].Split('|');
                ids[i]=Int32.Parse(temp[0]);
                names[i] = temp[1].Trim();
                unit[i] = Int32.Parse(temp[2]);
                qnty[i] = Int32.Parse(temp[3]);
                total[i]=Int32.Parse(temp[4]);
            }
            if (saveData())
            {
                try
                {
                    PrintOrder po= new PrintOrder();
                    po.SetParameterValue("OID",ordernumber);
                    po.SetParameterValue("order",localorder);
                    po.PrintToPrinter(1,false,0,0);
                    subPrintOrder spo= new subPrintOrder();
                    spo.SetParameterValue("o", ordernumber);
                    spo.SetParameterValue("lo", localorder);
                    spo.PrintToPrinter(1, false, 0, 0);
                    localorder++;
                    return Ok();
                }
                catch
                {
                    return BadRequest("Problem With the printer.");
                }
            }
            
            return BadRequest("Item is Ended. or the quantity is not good entered...");
        }

        private bool updateItem()
        {
            bool flag = false;
            for (int i = 0; i < ids.Length; i++)
            {
                int q = qnty[i];
                int temp = ids[i];
                item items = db.items.Find(temp);
                items.qty = items.qty - q;
                if (items.qty < 0)
                {
                    items.qty = items.qty + q;
                    flag = true;
                }
                items.lastsold = DateTime.Now;
            }
            if (!flag)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        private int calProfit()
        {
            int profit = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                int temp = ids[i];
                List<item> items = db.items.Where(x => x.id == temp).ToList();
                profit = profit + ((items[0].retail_price - items[0].original_price) * qnty[i]);
            }
            return profit;
        }
        private int getItemProfit(int id)
        {

            int temp = ids[id];
            item items = db.items.Where(i => i.id == temp).ToList()[0];
            return (qnty[id]) * (items.retail_price - items.original_price);
        }

        private void saveOrderItems(int oid)
        {
            order_itemsModel oic = new order_itemsModel();
            for (int i = 0; i < ids.Length; i++)
            {
                order_items oi = new order_items();
                oi.oid = oid;
                oi.item_id = ids[i];
                oi.qty = qnty[i];
                oi.total = total[i];
                oi.disc = 0;
                oi.profit = getItemProfit(i);
                oic.Create(oi);
            }
        }

        public bool saveData()
        {
            if (!updateItem())
            {
                return false;
            }
            else
            {
                order o = new order();
                o.cus_id = 1;
                o.user_id = User.Identity.GetUserId();
                o.total_profit = calProfit();
                o.total_amount = sale;
                o.rcv = rcv;
                ordernumber = new orderModel().Create(o);

                saveOrderItems(ordernumber);
                return true;
            }
        }
    }
}
