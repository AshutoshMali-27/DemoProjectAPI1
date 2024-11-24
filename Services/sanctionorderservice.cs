using IRepositroy;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class sanctionorderservice : ISanctionOrder
    {
        private readonly IConfiguration configuration;
        private readonly string dbconnection;
        public sanctionorderservice(IConfiguration configuration)
        {

            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<List<FinancialYearViewModel>> GetFinancialYear()
        {
            var finyearitem = new List<FinancialYearViewModel>();

            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetFinancialYear", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Financialyearitem = new FinancialYearViewModel
                            {
                                id = Convert.ToInt32(reader["id"]),
                                FinancialYear = reader["FinancialYear"].ToString(),
                               
                            };

                            finyearitem.Add(Financialyearitem);
                        }
                    }
                }
            }

            return finyearitem;
        }

        public async Task<List<SanctionOrderDetails>> Getsanctionorderdetails()
        {
            var sanctionorderdetailforget = new List<SanctionOrderDetails>();

            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetSanctionOrderAmounts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var SanctionOrderde = new SanctionOrderDetails
                            {
                                SanctionAmount = Convert.ToDouble(reader["SanctionAmount"]),
                                BalanceAmount = Convert.ToDouble(reader["BalanceAmount"]),
                                ExpenditureAmount = Convert.ToDouble(reader["ExpenditureAmount"])
                            };

                            sanctionorderdetailforget.Add(SanctionOrderde);
                        }
                    }
                }
            }

            return sanctionorderdetailforget;
        }

        public async Task<List<Scheme>> GetSchemeNames()
        {
            var SchemeName = new List<Scheme>();

            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetSchemes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var SchemeType = new Scheme
                            {
                                id = Convert.ToInt32(reader["id"]),
                                Schemename = reader["Schemename"].ToString(),

                            };

                            SchemeName.Add(SchemeType);
                        }
                    }
                }
            }

            return SchemeName;
        }

        public async Task<List<Scheme>> GetComponentNames(int schemeid)
        {
            var componentNames = new List<Scheme>();

            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GETComponent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the schemeid parameter to the stored procedure
                    command.Parameters.AddWithValue("@schemeid", schemeid);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var scheme = new Scheme
                            {
                                id = Convert.ToInt32(reader["id"]),
                                ComponentName = reader["ComponentName"].ToString(),
                            };

                            componentNames.Add(scheme);
                        }
                    }
                }
            }

            return componentNames;
        }

        public async Task<int> InsertSanctionOrderEntry(int financialYearId, int schemeId, int componentId,int utypeid,int ulbid,double SanctionAmount,double ExpenditureAmount,double BalanceAmount,double amount,string Sanctionnumber, string sanctionfileupload)
        {
            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("InsertSanctionOrderEntry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    command.Parameters.AddWithValue("@FinancialYearId", financialYearId);
                    command.Parameters.AddWithValue("@SchemeId", schemeId);
                    command.Parameters.AddWithValue("@ComponentId", componentId);
                    command.Parameters.AddWithValue("@utypeid", utypeid);
                    command.Parameters.AddWithValue("@ulbid", ulbid);
                    command.Parameters.AddWithValue("@SanctionAmount", SanctionAmount);
                    command.Parameters.AddWithValue("@ExpenditureAmount", ExpenditureAmount);
                    command.Parameters.AddWithValue("@BalanceAmount", BalanceAmount);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@sanctionnumber", Sanctionnumber);
                    command.Parameters.AddWithValue("@sanctionfileupload", sanctionfileupload);
                    
                    // Execute the stored procedure and get the new ID
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task<List<ClsSanctionorderdetails>> GetSanctionOrderDetailinbox()
        {

            var sanctionorderdetailinbox = new List<ClsSanctionorderdetails>();

            using (var connection = new SqlConnection(dbconnection))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetSantionOrderDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var sanctiondetailinbox = new ClsSanctionorderdetails
                            {
                                id = Convert.ToInt32(reader["id"]),
                                FinancialYear = Convert.ToString(reader["FinancialYear"]),
                                Schemename = Convert.ToString(reader["Schemename"]),
                                ComponentName = Convert.ToString(reader["ComponentName"]),
                                amount = Convert.ToDouble(reader["amount"]),
                                Sanctionnumber = Convert.ToString(reader["Sanctionnumber"]),
                                ApprovalStatus=Convert.ToString(reader["ApprovalStatus"])
                            };

                            sanctionorderdetailinbox.Add(sanctiondetailinbox);
                        }
                    }
                }
            }

            return sanctionorderdetailinbox;
        }
    }
}
