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
    
    public partial class Specialist
    {
        public Specialist()
        {
            this.ProjSpecialist = new HashSet<ProjSpecialist>();
        }
    
        public int specialistId { get; set; }
        public string name { get; set; }
        public int areaId { get; set; }
        public int ci { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual ICollection<ProjSpecialist> ProjSpecialist { get; set; }
    }
}