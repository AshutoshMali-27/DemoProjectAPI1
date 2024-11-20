using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class SanctionOrderEntryViewModel
    {
        public int FinancialYearId { get; set; }

        public int SchemeId { get; set; }

        public int ComponentId { get; set; }
        public int utypeid {  get; set; }
        public int ulbid {  get; set; }
        public double ExpenditureAmount { get; set; }
        public double SanctionAmount { get; set; }
        public double BalanceAmount { get; set; }
    }
}
