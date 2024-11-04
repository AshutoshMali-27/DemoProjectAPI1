using DAL;
using DemoProjectAPI1;
using IRepositroy;
using Microsoft.Extensions.Configuration;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoginService : IloginInterface
    {
        private readonly IConfiguration configuration;
        private readonly string dbconnection;
        public LoginService(IConfiguration configuration)
        {
            
            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DefaultConnection"];
        }




        public ClsLoginModel Login(string username, string password)
        {
            string storedProcedureName = "GETuserDetail"; // Name of your stored procedure
            ClsLoginModel user = null;

            using (var connection = new SqlConnection(dbconnection))
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@Username", username));
                    command.Parameters.Add(new SqlParameter("@Password", password));

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Read the first record
                        {
                            user = new ClsLoginModel
                            {
                                id = reader["id"] != DBNull.Value ? (int?)reader["id"] : null,
                                Username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : null,
                                Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null,
                                UtypeId = reader["UtypeId"] != DBNull.Value ? (int?)reader["UtypeId"] : null,
                                EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : null,
                                ApprovalStatus = reader["ApprovalStatus"] != DBNull.Value ? (int?)reader["ApprovalStatus"] : null,
                                IsActive = reader["IsActive"] != DBNull.Value ? (int?)reader["IsActive"] : null,
                                Ulbid = reader["Ulbid"] != DBNull.Value ? (int?)reader["Ulbid"] : null
                            };
                        }
                    }
                }
            }

            return user; // Return the user found or null if not found
        }




    }
}
