using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ClsSanctionorderdetails
    {
        public int id {get;set;}
        public string FinancialYear { get;set;}

        public string Schemename { get; set; }
        public string ComponentName { get; set; }

        public double amount { get; set; }

        public string Sanctionnumber {  get; set; }

        public string ApprovalStatus { get;set; }

    }
}
