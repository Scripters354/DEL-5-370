//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THe_BOok_MArket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Line
    {
        public int InvSuppOrder_ID { get; set; }
        public int Inventory_ID { get; set; }
        public Nullable<int> SuppOrder_Status_ID { get; set; }
        public Nullable<int> Quanity { get; set; }
        public Nullable<double> Line_Total { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Inventory Inventory { get; set; }
        public virtual Inventory_Supplier_Order Inventory_Supplier_Order { get; set; }
        public virtual Order_Status Order_Status { get; set; }
    }
}
