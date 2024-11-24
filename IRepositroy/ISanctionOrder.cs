using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositroy
{
    public interface ISanctionOrder
    {
        Task<List<FinancialYearViewModel>> GetFinancialYear ();
        Task<List<SanctionOrderDetails>> Getsanctionorderdetails();
        Task<List<Scheme>> GetSchemeNames();
        Task<List<Scheme>> GetComponentNames(int schemeid);
        Task<List<ClsSanctionorderdetails>> GetSanctionOrderDetailinbox();
        Task<int> InsertSanctionOrderEntry(int financialYearId, int schemeId, int componentId, int utypeid, int ulbid, double SanctionAmount, double ExpenditureAmount, double BalanceAmount,double amount,string Sanctionnumber, string sanctionfileupload);
    }
}
