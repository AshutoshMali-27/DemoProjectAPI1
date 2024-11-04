using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositroy
{
    public interface IDAL
    {
        int Execute(string sql, params SqlParameter[] parameters);

        List<T> ExecuteStoredProcedure<T>(string storedProcedureName, Func<SqlDataReader, T> map, params SqlParameter[] parameters);
        List<T> GetData<T>(string sql, Func<IDataReader, T> map, params SqlParameter[] parameters);
    }
}
