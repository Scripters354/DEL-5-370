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
    
    public partial class Return_Supplier_Order
    {
        public Return_Supplier_Order()
        {
            this.Return_Line = new HashSet<Return_Line>();
        }
    
        public int ReturnSupp_Order_ID { get; set; }
        public Nullable<int> InvSuppOrder_ID { get; set; }
        public Nullable<int> Quanity { get; set; }
        public string Reason_For_Return { get; set; }
    
        public virtual Inventory_Supplier_Order Inventory_Supplier_Order { get; set; }
        public virtual ICollection<Return_Line> Return_Line { get; set; }
    }
}
