using System.Data;
using System.Threading.Tasks;
using App.Common.Exception;
using MySql.Data.MySqlClient;

namespace App.Common.Utility
{
    public interface IMysqlConnector
    {
        Task<DataSet> GetDataSetAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null,
            string tableName = "Table1", int timeout = 180);

        Task<DataTable> GetDataTableAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null,
            int timeout = 180);

        Task<bool> ExecuteNonQueryAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null, int timeout = 180);

        Task<object> ExecuteScalarAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null,
            int timeout = 180);
    }

    public class MysqlConnector : IMysqlConnector
    {
        public async Task<DataSet> GetDataSetAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null, string tableName = "Table1", int timeout = 180)
        {
            var ds = new DataSet();

            try
            {
                var cmd = new MySqlCommand(sql, conn) { CommandTimeout = timeout };
                if (parameter != null)
                {
                    foreach (var tmp in parameter)
                    {
                        cmd.Parameters.Add(tmp);
                    }
                }
                conn.Open();
                var adapter = new MySqlDataAdapter(cmd);

                await adapter.FillAsync(ds, tableName);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
            finally
            {
                conn?.Close();
            }

            return ds;
        }

        public async Task<DataTable> GetDataTableAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null, int timeout = 180)
        {
            var ds = new DataTable();

            try
            {
                var cmd = new MySqlCommand(sql, conn) { CommandTimeout = timeout };
                if (parameter != null)
                {
                    foreach (var tmp in parameter)
                    {
                        cmd.Parameters.Add(tmp);
                    }
                }
                conn.Open();
                var adapter = new MySqlDataAdapter(cmd);
                await adapter.FillAsync(ds);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
            finally
            {
                conn?.Close();
            }

            return ds;
        }

        public async Task<bool> ExecuteNonQueryAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null, int timeout = 180)
        {
            var iResults = 0;

            try
            {
                var cmd = new MySqlCommand(sql, conn) { CommandTimeout = timeout };

                if (parameter != null)
                {
                    foreach (var tmp in parameter)
                    {
                        cmd.Parameters.Add(tmp);
                    }
                }
                cmd.Connection.Open();
                iResults = await cmd.ExecuteNonQueryAsync();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
            finally
            {
                conn?.Close();
            }
            return iResults>=0;
        }

        public async Task<object> ExecuteScalarAsync(MySqlConnection conn, string sql, MySqlParameter[] parameter = null, int timeout = 180)
        {
            object objResults;

            try
            {
                var cmd = new MySqlCommand(sql, conn) { CommandTimeout = timeout };
                if (parameter != null)
                {
                    foreach (var tmp in parameter)
                    {
                        cmd.Parameters.Add(tmp);
                    }
                }
                cmd.Connection.Open();
                objResults = await cmd.ExecuteScalarAsync();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
            finally
            {
                conn?.Close();
            }

            return objResults;
        }
    }
}