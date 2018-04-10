using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GPOSDAL;

namespace GPOS.Models
{
    public class itemModel : IDisposable
    {
        private static bool UpdateDatabase = false;
        private GPOSEntities entities=new GPOSEntities();

        public IList<item> GetAll()
        {
            IList<item> result = new List<item>();

            result = entities.items.ToList();

            return result;
        }
        public IEnumerable<item> Read()
        {
            return GetAll();
        }

        public void Create(item product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                product.id = id + 1;

                entities.items.Add(product);
                entities.SaveChanges();
            }
            else
            {
                var entity = new item();

                entity.bid = product.bid;
                entity.name = product.name;
                entity.Barcode = product.Barcode;
                entity.description = product.description;
                entity.company = product.company;
                entity.retail_price = product.retail_price;
                entity.original_price = product.original_price;
                entity.discount_price = product.discount_price;
                entity.qty = product.qty;
                entity.lastsold = product.lastsold;
                entity.tag = product.tag;
                entity.isActive = product.isActive;

                entities.items.Add(entity);
                entities.SaveChanges();

                product.id = entity.id;
            }
        }

        public void Update(item product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == product.id);

                if (target != null)
                {
                    target.bid = product.bid;
                    target.name = product.name;
                    target.Barcode = product.Barcode;
                    target.description = product.description;
                    target.company = product.company;
                    target.retail_price = product.retail_price;
                    target.original_price = product.original_price;
                    target.discount_price = product.discount_price;
                    target.qty = product.qty;
                    target.lastsold = product.lastsold;
                    target.tag = product.tag;
                    target.isActive = product.isActive;
                }
                entities.SaveChanges();
            }
            else
            {
                var entity = new item();

                entity.bid = product.bid;
                entity.name = product.name;
                entity.Barcode = product.Barcode;
                entity.description = product.description;
                entity.company = product.company;
                entity.retail_price = product.retail_price;
                entity.original_price = product.original_price;
                entity.discount_price = product.discount_price;
                entity.qty = product.qty;
                entity.lastsold = product.lastsold;
                entity.tag = product.tag;
                entity.isActive = product.isActive;

                entities.items.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(item product)
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

        public item One(Func<item, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }
        public void Dispose()
        {
            entities.Dispose();
        }
    }
}