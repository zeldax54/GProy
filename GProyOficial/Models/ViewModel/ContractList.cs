using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GProyOficial.Models.ViewModel
{
    public class ContractList
    {
        public ICollection<Contract> Contracts { get; set; }
        public Contract Contract { get; set; }
        public int? ClientId { get; set; }
    }
}