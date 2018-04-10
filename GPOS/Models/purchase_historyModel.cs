using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using GPOSDAL;

namespace GPOS.Models
{
    //[Authorize(Roles = "admin")]
    public class purchase_historyModel : IDisposable
    {
        private static bool UpdateDatabase = false;
        private GPOSEntities entities = new GPOSEntities();

        public IList<purchase_history> GetAll()
        {
            IList<purchase_history> result = new List<purchase_history>();

            result = entities.purchase_history.ToList();

            return result;
        }

        public IEnumerable<purchase_history> Read()
        {
            return GetAll();
        }

        public void Create(purchase_history product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                product.id = id + 1;

                entities.purchase_history.Add(product);
                entities.SaveChanges();
            }
            else
            {
                var entity = new purchase_history();

                entity.item_id = product.item_id;
                entity.dt=DateTime.Now;
                entity.qty = product.qty;
                entity.price = product.price;

                entities.purchase_history.Add(entity);
                entities.SaveChanges();

                product.id = entity.id;
            }
        }

        public void Update(purchase_history product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == product.id);

                if (target != null)
                {
                    target.item_id = product.item_id;
                    target.dt = DateTime.Now;
                    target.qty = product.qty;
                    target.price = product.price;
                }
                entities.SaveChanges();
            }
            else
            {
                var entity = new purchase_history();

                entity.item_id = product.item_id;
                entity.dt = DateTime.Now;
                entity.qty = product.qty;
                entity.price = product.price;

                entities.purchase_history.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
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

        public purchase_history One(Func<purchase_history, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities?.Dispose();
        }
    }
}