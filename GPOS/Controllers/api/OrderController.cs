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
        
        private int sale;
        private int[] ids;
        private String[] names;
        private int[] unit;
        private int[] qnty;
        private int[] total;
        private int rcv;
        private int blnc;
        private int ordernumber;
        private int customer_id;

        public IHttpActionResult SetOrder([FromUri] string items, [FromUri] int tot, [FromUri] int dis,
            [FromUri] int rcv, [FromUri] int cus_id)
        {
            customer_id = cus_id;
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
                    ReceiptReport po = new ReceiptReport();
                    po.SetParameterValue("OID", ordernumber);
                    po.SetParameterValue("discount", dis);
                    po.PrintToPrinter(1, false, 0, 0);
                    return Ok();
                }
                catch
                {
                    return BadRequest("Problem With the printer.");
                }
            }
            else
            {
                return BadRequest("Item is Ended. or the quantity is not good entered...");
            }
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
                o.cus_id = customer_id;
                o.user_id = User.Identity.GetUserId();
                o.total_profit = calProfit();
                o.total_amount = sale;
                o.rcv = rcv;
                ordernumber = new orderModel().Create(o);

                saveOrderItems(ordernumber);
                return true;
            }
        }

        public IHttpActionResult ReprintOrder([FromUri]int id)
        {
            ReceiptReport po = new ReceiptReport();
            //po.SetParameterValue("OID", id);
            //po.PrintToPrinter(1, false, 0, 0);
            return Ok();
        }
        public IHttpActionResult createCustomer([FromUri] string customer_name, [FromUri] string customer_phone, [FromUri] string customer_address )
        {
            customer ncus = new customer();
            ncus.name = customer_name;
            ncus.address = customer_address;
            ncus.phone = customer_phone;
            ncus.isActive = true;
            ncus.tag = true;
            ncus.bid = 1;
            ncus.balance = 0;
            int id = new customerModel().Create(ncus);
            if (id <= 0)
                return BadRequest("Error adding the Customer");
            return Ok(id);
        }
        
        public IHttpActionResult Return_item([FromUri]int item_id, [FromUri]int qty, [FromUri]int oid)
        {
            order o = new orderModel().GetAll().Where(x => x.id == oid).ToList()[0];
            order_items oi = new order_itemsModel().GetAll().Where(x => x.oid == o.id && x.item_id == item_id).ToList()[0];
            oi.qty = oi.qty - qty;
            oi.total = oi.total - oi.item.retail_price * qty;
            oi.profit = oi.profit - (oi.item.retail_price - oi.item.original_price) * qty;
            //oi.disc = oi.disc - oi.item.discount_price * qty;
            new order_itemsModel().Update(oi);
            o.total_amount = o.total_amount - (oi.item.retail_price * qty);
            o.total_profit = o.total_profit - (oi.item.retail_price - oi.item.original_price) * qty;
            new orderModel().Update(o);
            order_return or = new order_return();
            or.oid = oid;
            or.dt = DateTime.Now;
            or.user_id = User.Identity.GetUserId();
            int id = new order_returnModel().Create(or);
            order_return_items ori = new order_return_items();
            ori.orid = id;
            ori.item_id = item_id;
            ori.qty = qty;
            new order_return_itemModel().Create(ori);
            item i = new itemModel().GetAll().Where(x => x.id == item_id).ToList()[0];
            i.qty = i.qty + qty;
            new itemModel().Update(i);
            return Ok();
        }
    }
}
