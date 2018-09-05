using GPOSDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GPOS.Models
{
    public class order_return_itemModel : IDisposable
    {
        private static bool UpdateDatabase = false;
        private GPOSEntities entities = new GPOSEntities();

        public IList<order_return_items> GetAll()
        {
            IList<order_return_items> result = new List<order_return_items>();

            result = entities.order_return_items.ToList();

            return result;
        }

        public IEnumerable<order_return_items> Read()
        {
            return GetAll();
        }

        public int Create(order_return_items product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                product.id = id + 1;

                entities.order_return_items.Add(product);
                entities.SaveChanges();
            }
            else
            {
                var entity = new order_return_items();

                entity.orid = product.orid;
                entity.item_id = product.item_id;
                entity.qty = product.qty;

                entities.order_return_items.Add(entity);
                entities.SaveChanges();

                product.id = entity.id;
            }
            return product.id;
        }

        public void Update(order_return_items product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == product.id);

                if (target != null)
                {
                    target.orid = product.orid;
                    target.item_id = product.item_id;
                    target.qty = product.qty;
                }
                entities.SaveChanges();
            }
            else
            {
                var entity = new order_return_items();

                entity.orid = product.orid;
                entity.item_id = product.item_id;
                entity.qty = product.qty;

                entities.order_return_items.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(order_return or)
        {

        }

        public order_return_items One(Func<order_return_items, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities?.Dispose();
        }
    }
}