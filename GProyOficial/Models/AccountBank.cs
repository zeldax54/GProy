//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GProyOficial.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountBank
    {
        public Nullable<int> clientId { get; set; }
        public int bankId { get; set; }
        public int currencyTypeId { get; set; }
        public long accountNumber { get; set; }
        public string titular { get; set; }
    
        public virtual Bank Bank { get; set; }
        public virtual Client Client { get; set; }
        public virtual CurrencyType CurrencyType { get; set; }
    }
}
