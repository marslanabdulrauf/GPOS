//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GPOSDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class order_items
    {
        public int oid { get; set; }
        public int item_id { get; set; }
        public int id { get; set; }
        public int qty { get; set; }
        public int total { get; set; }
        public int profit { get; set; }
        public int disc { get; set; }
    
        public virtual item item { get; protected set; }
        public virtual order order { protected get; set; }
    }
}