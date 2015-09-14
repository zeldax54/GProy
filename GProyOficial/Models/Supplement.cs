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
    
    public partial class Supplement
    {
        public Supplement()
        {
            this.Attached = new HashSet<Attached>();
            this.ClientSupplement = new HashSet<ClientSupplement>();
            this.ProjSup = new HashSet<ProjSup>();
            this.StateCSupplement = new HashSet<StateCSupplement>();
            this.SupplementCurrencyType = new HashSet<SupplementCurrencyType>();
        }
    
        public int supplementId { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public int contractId { get; set; }
        public decimal amount { get; set; }
        public string nom1 { get; set; }
        public string nom2 { get; set; }
        public Nullable<System.DateTime> signedClient { get; set; }
        public Nullable<int> productId { get; set; }
        public int serviceId { get; set; }
        public System.DateTime signedProvider { get; set; }
        public Nullable<int> comitteNumber { get; set; }
        public Nullable<System.DateTime> comitteDate { get; set; }
        public System.DateTime expirationDate { get; set; }
    
        public virtual ICollection<Attached> Attached { get; set; }
        public virtual ICollection<ClientSupplement> ClientSupplement { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProjSup> ProjSup { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<StateCSupplement> StateCSupplement { get; set; }
        public virtual ICollection<SupplementCurrencyType> SupplementCurrencyType { get; set; }
    }
}