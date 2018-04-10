using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GPOSDAL;

namespace GPOS.Models
{
    public class orderModel : IDisposable
    {
        private static bool UpdateDatabase = false;
        private GPOSEntities entities = new GPOSEntities();

        public IList<order> GetAll()
        {
            IList<order> result = new List<order>();

            result = entities.orders.ToList();

            return result;
        }

        public IEnumerable<order> Read()
        {
            return GetAll();
        }

        public int Create(order product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                product.id = id + 1;
                product.dt=DateTime.Now;
                entities.orders.Add(product);
                entities.SaveChanges();
                return product.id;
            }
            else
            {
                var entity = new order();

                entity.cus_id = product.cus_id;
                entity.user_id = product.user_id;
                entity.dt = DateTime.Now;
                entity.total_amount = product.total_amount;
                entity.total_profit = product.total_profit;
                entity.disc = product.disc;
                entity.rcv = product.rcv;

                entities.orders.Add(entity);
                entities.SaveChanges();

                product.id = entity.id;
                return product.id;
            }
        }

        public void Update(order product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == product.id);

                if (target != null)
                {
                    target.cus_id = product.cus_id;
                    target.user_id = product.user_id;
                    target.dt = product.dt;
                    target.total_amount = product.total_amount;
                    target.total_profit = product.total_profit;
                    target.disc = product.disc;
                    target.rcv = product.rcv;
                }
                entities.SaveChanges();
            }
            else
            {
                var entity = new order();

                entity.cus_id = product.cus_id;
                entity.user_id = product.user_id;
                entity.dt = product.dt;
                entity.total_amount = product.total_amount;
                entity.total_profit = product.total_profit;
                entity.disc = product.disc;
                entity.rcv = product.rcv;

                entities.orders.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(customer product)
        {
            //if (!UpdateDatabase)
            //{
            //    var target = GetAll().FirstOrDefault(p => p.id == product.id);
            //    if (target != null)
            //    {
            //        GetAll().Remove(target);
            //    }//}
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
        public order One(Func<order, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {

            entities?.Dispose();
        }
    }
}