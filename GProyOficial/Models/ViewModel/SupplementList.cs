using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GProyOficial.Models.ViewModel
{
    public class SupplementList
    {
        public ICollection<Supplement> Supplements { get; set; }
        public Supplement Supplement { get; set; }
        public int? ContractId { get; set; }
        public bool IsClient { get; set; }
        public int ClientId { get; set; }
    }
}