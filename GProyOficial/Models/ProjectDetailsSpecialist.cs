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
    
    public partial class ProjectDetailsSpecialist
    {
        public ProjectDetailsSpecialist()
        {
            this.InvoiceProjectDetails = new HashSet<InvoiceProjectDetails>();
        }
    
        public int projSpecialistId { get; set; }
        public int projectDetailsId { get; set; }
        public int projDetailsSpecialistId { get; set; }
        public double participationPercent { get; set; }
    
        public virtual ICollection<InvoiceProjectDetails> InvoiceProjectDetails { get; set; }
        public virtual ProjectDetails ProjectDetails { get; set; }
        public virtual ProjSpecialist ProjSpecialist { get; set; }
    }
}