using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GPOSDAL;

namespace GPOS.Models
{
    public class order_itemsModel : IDisposable
    {
        private static bool UpdateDatabase = false;
        private GPOSEntities entities = new GPOSEntities();


        public IList<order_items> GetAll()
        {
            IList<order_items> result = new List<order_items>();

            result = entities.order_items.ToList();

            return result;
        }

        public IEnumerable<order_items> Read()
        {
            return GetAll();
        }

        public void Create(order_items product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                product.id = id + 1;

                entities.order_items.Add(product);
                entities.SaveChanges();
            }
            else
            {
                var entity = new order_items();

                entity.item_id = product.item_id;
                entity.oid = product.oid;
                entity.qty = product.qty;
                entity.total = product.total;
                entity.profit = product.profit;
                entity.disc = product.disc;

                entities.order_items.Add(entity);
                entities.SaveChanges();

                product.id = entity.id;
            }
        }

        public void Update(purchase_history product)
        {
            //is function ko khud implement krna pare ga requirement k mutabik.

            //if (!UpdateDatabase)
            //{
            //    var target = One(e => e.id == product.id);

            //    if (target != null)
            //    {
            //        target.item_id = product.item_id;
            //        target.dt = DateTime.Now;
            //        target.qty = product.qty;
            //        target.price = product.price;
            //    }
            //    entities.SaveChanges();
            //}
            //else
            //{
            //    var entity = new purchase_history();

            //    entity.item_id = product.item_id;
            //    entity.dt = DateTime.Now;
            //    entity.qty = product.qty;
            //    entity.price = product.price;

            //    entities.purchase_history.Attach(entity);
            //    entities.Entry(entity).State = EntityState.Modified;
            //    entities.SaveChanges();
            //}
        }

        public void Destroy(purchase_history product)
        {
            //if (!UpdateDatabase)
            //{
            //    var target = GetAll().FirstOrDefault(p => p.id == product.id);
            //    if (target != null)
            //    {
            //        GetAll().Remove(target);
            //    }
            //}
            //else
            //{
            //    var entity = new Product();

            //    entity.ProductID = product.ProductID;

            //    entities.Products.Attach(entity);

            //    entities.Products.Remove(entity);

            //    var orderDetails = entities.Order_Details.Where(pd => pd.ProductID == entity.ProductID);

            //    foreach (var orderDetail in orderDetails)
            //    {
            //        entities.Order_Details.Remove(orderDetail);
            //    }

            //    entities.SaveChanges();
            //}
        }

        public void Dispose()
        {
            entities?.Dispose();
        }
    }
}