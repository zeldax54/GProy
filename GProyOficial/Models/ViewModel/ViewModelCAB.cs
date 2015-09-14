using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GProyOficial.Models.ViewModel
{
    public class ViewModelCAB
    {
        public Contract Contract { get; set; }
        public List<Contract> Contracts { get; set; } 
        public StateContract StateContract { get; set; }
    }
}