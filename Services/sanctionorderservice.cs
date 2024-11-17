using IRepositroy;
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
    }
}
