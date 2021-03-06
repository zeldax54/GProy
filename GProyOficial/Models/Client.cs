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
    
    public partial class Client
    {
        public Client()
        {
            this.AccountBank = new HashSet<AccountBank>();
            this.Client1 = new HashSet<Client>();
            this.Invoice = new HashSet<Invoice>();
            this.ClientSupplement = new HashSet<ClientSupplement>();
            this.Contract = new HashSet<Contract>();
            this.Project = new HashSet<Project>();
        }
    
        public int clientId { get; set; }
        public Nullable<System.DateTime> dateCreation { get; set; }
        public string name { get; set; }
        public string identifAbrev { get; set; }
        public string reup { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string agent { get; set; }
        public string agentPosition { get; set; }
        public int organismId { get; set; }
        public string country { get; set; }
        public bool isSubject { get; set; }
        public Nullable<int> fatherId { get; set; }
        public bool legalPerson { get; set; }
    
        public virtual ICollection<AccountBank> AccountBank { get; set; }
        public virtual ICollection<Client> Client1 { get; set; }
        public virtual Client Client2 { get; set; }
        public virtual Organism Organism { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<ClientSupplement> ClientSupplement { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}
