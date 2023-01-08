using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RepairCardsDapperData.Data
{
    public class RepoBase : IDisposable
    {
        protected IDbConnection conn;

        public RepoBase()
        {
            conn = new SqlConnection("");
            conn.Open();
            SqlMapper.Settings.CommandTimeout = 90;
        }

        public void Dispose() => conn.Dispose();
    }
}
