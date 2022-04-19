using Application.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Infrastructure.Persistence
{
    public class SqliteDataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public SqliteDataAccess(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadData<T>(string sql, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                List<T> rows = cnn.Query<T>(sql).ToList();

                return rows;
            }
        }

        public void SaveData(string sql, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Execute(sql);
            }
        }
    }
}
