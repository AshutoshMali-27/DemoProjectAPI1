using IRepositroy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL : IDAL
    {
        private readonly IConfiguration configuration;
        private readonly string dbconnection;
        public DAL(IConfiguration configuration) {
            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DefaultConnection"];
        }

        public int Execute(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(dbconnection))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public List<T> GetData<T>(string sql, Func<IDataReader, T> map, params SqlParameter[] parameters)
        {
            var results = new List<T>();

            using (var connection = new SqlConnection(dbconnection))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(map(reader));
                        }
                    }
                }
            }

            return results;
        }


            public List<T> ExecuteStoredProcedure<T>(string storedProcedureName, Func<SqlDataReader, T> map, params SqlParameter[] parameters)
            {
                var results = new List<T>();

                using (var connection = new SqlConnection(dbconnection))
                {
                    using (var command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure; // Specify that this is a stored procedure
                        command.Parameters.AddRange(parameters); // Add parameters to the command
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(map(reader)); // Use the mapping function to create objects
                            }
                        }
                    }
                }

                return results;
            }

    }
}
