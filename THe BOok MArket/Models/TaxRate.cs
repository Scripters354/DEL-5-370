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
    
    public partial class TaxRate
    {
        public TaxRate()
        {
            this.Sales = new HashSet<Sale>();
        }
    
        public int TaxRate_ID { get; set; }
        public string Tax_Description { get; set; }
        public Nullable<int> Tax_Percent { get; set; }
    
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
