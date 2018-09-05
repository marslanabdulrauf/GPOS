using GPOSDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GPOS.Models
{
    public class order_returnModel : IDisposable
    {
        private static bool UpdateDatabase = false;
        private GPOSEntities entities = new GPOSEntities();

        public IList<order_return> GetAll()
        {
            IList<order_return> result = new List<order_return>();

            result = entities.order_return.ToList();

            return result;
        }

        public IEnumerable<order_return> Read()
        {
            return GetAll();
        }

        public int Create(order_return product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                product.id = id + 1;

                entities.order_return.Add(product);
                entities.SaveChanges();
            }
            else
            {
                var entity = new order_return();

                entity.oid = product.oid;
                entity.dt = product.dt;
                entity.user_id = product.user_id;

                entities.order_return.Add(entity);
                entities.SaveChanges();

                product.id = entity.id;
            }
            return product.id;
        }

        public void Update(order_return product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == product.id);

                if (target != null)
                {
                    target.oid = product.oid;
                    target.user_id = product.user_id;
                    target.dt = product.dt;
                }
                entities.SaveChanges();
            }
            else
            {
                var entity = new order_return();

                entity.oid = product.oid;
                entity.user_id = product.user_id;
                entity.dt = product.dt;

                entities.order_return.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(order_return or)
        {
             
        }

        public order_return One(Func<order_return, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities?.Dispose();
        }
    }
}