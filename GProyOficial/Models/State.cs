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
    
    public partial class State
    {
        public State()
        {
            this.InvoiceStateSet = new HashSet<InvoiceStateSet>();
            this.ProjectState = new HashSet<ProjectState>();
        }
    
        public int stateId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    
        public virtual ICollection<InvoiceStateSet> InvoiceStateSet { get; set; }
        public virtual ICollection<ProjectState> ProjectState { get; set; }
    }
}