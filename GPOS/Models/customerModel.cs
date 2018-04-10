using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GPOSDAL;

namespace GPOS.Models
{
    public class customerModel : IDisposable
    {
        private static bool UpdateDatabase = false;
        private GPOSEntities entities = new GPOSEntities();

        public IList<customer> GetAll()
        {
            IList<customer> result = new List<customer>();

            result = entities.customers.ToList();

            return result;
        }

        public IEnumerable<customer> Read()
        {
            return GetAll();
        }

        public void Create(customer product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                product.id = id + 1;

                entities.customers.Add(product);
                entities.SaveChanges();
            }
            else
            {
                var entity = new customer();

                entity.bid = product.bid;
                entity.name = product.name;
                entity.address = product.address;
                entity.phone = product.phone;
                entity.balance = product.balance;
                entity.tag = product.tag;

                entities.customers.Add(entity);
                entities.SaveChanges();

                product.id = entity.id;
            }
        }

        public void Update(customer product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == product.id);

                if (target != null)
                {
                    target.bid = product.bid;
                    target.name = product.name;
                    target.address = product.address;
                    target.phone = product.phone;
                    target.balance = product.balance;
                    target.tag = product.tag;
                }
                entities.SaveChanges();
            }
            else
            {
                var entity = new customer();

                entity.bid = product.bid;
                entity.name = product.name;
                entity.address = product.address;
                entity.phone = product.phone;
                entity.balance = product.balance;
                entity.tag = product.tag;

                entities.customers.Attach(entity);
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

        public customer One(Func<customer, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities?.Dispose();
        }
    }
}