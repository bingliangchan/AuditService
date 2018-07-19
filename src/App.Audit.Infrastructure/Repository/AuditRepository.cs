using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Audit.Domain;
using App.Audit.Domain.Model;
using App.Common.Exception;
using App.Common.Repository;
using App.Common.Utility;
using MySql.Data.MySqlClient;

namespace App.Audit.Infrastructure.Repository
{
    public interface IAuditRepository : IBaseRepository<AuditEvent>
    {
        Task<List<AuditEvent>> GetAuditLogsAsync(int? queryUserId, DateTime? queryStartDate, DateTime? queryEndDate);

        Task<AuditEvent> GetLastRecordAsync();

        Task<bool> DeleteByUserIdAsync(int userId);

    }

    public class AuditRepository : IAuditRepository
    {
        private readonly IMysqlConnector _mysqlConnector;
       
        public AuditRepository(IMysqlConnector mysqlConnector)
        {
            _mysqlConnector = mysqlConnector;
        }

        public async Task<bool> AddAsync(AuditEvent value)
        {
            var connectionString = Constants.SqlConn;
            var sql = "insert into `audit_event` (event_name, event_unique_id ,event_source,event_time,request_origin,request_action, request_headers,request_source,user_id,created) " +
                      "values(@event_name, @event_unique_id ,@event_source,@event_time,@request_origin,@request_action, @request_headers,@request_source,@user_id,now()); " +
                      "insert into `audit_event_payload` (request_payload_type,request_payload_content,event_id,created) " +
                      "value(@request_payload_type,@request_payload_content,LAST_INSERT_ID(),now());";
            var conn = new MySqlConnection(connectionString);
            var queryParameterList = new List<MySqlParameter>
            {
                new MySqlParameter("@event_name", MySqlDbType.VarChar) {Value = value.EventName},
                new MySqlParameter("@event_unique_id", MySqlDbType.VarChar) {Value = value.EventUniqueId},
                new MySqlParameter("@event_source", MySqlDbType.VarChar) {Value = value.EventSource},
                new MySqlParameter("@event_time", MySqlDbType.DateTime) {Value = value.EventTime},
                new MySqlParameter("@request_origin", MySqlDbType.VarChar) {Value = value.RequestOrigin},
                new MySqlParameter("@request_action", MySqlDbType.VarChar) {Value = value.RequestAction},
                new MySqlParameter("@request_headers", MySqlDbType.VarChar) {Value = value.RequestHeaders},
                new MySqlParameter("@request_source", MySqlDbType.VarChar) {Value = value.RequestSource},
                new MySqlParameter("@user_id", MySqlDbType.Int32) {Value = value.UserId},
                new MySqlParameter("@request_payload_type", MySqlDbType.VarChar) {Value = value.EventPayload.RequestPayLoadType},
                new MySqlParameter("@request_payload_content", MySqlDbType.Text) {Value = value.EventPayload.RequestPayLoadContent},
            };

            var result = await _mysqlConnector.ExecuteNonQueryAsync(conn, sql, queryParameterList.ToArray());

            if (!result)
                throw new RepositoryException("Cannot Add Records");

            return result;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AuditEvent value)
        {
            throw new NotImplementedException();
        }

        public Task<AuditEvent> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AuditEvent>> GetAuditLogsAsync(int? queryUserId, DateTime? queryStartDate, DateTime? queryEndDate)
        {
            var connectionString = Constants.SqlConn;
            var sql = "select audit_event.*, audit_event_payload.* from `audit_event`  " +
                      "inner join `audit_event_payload` on audit_event_payload.event_id=`audit_event`.id " +
                      "where `audit_event`.user_id=@user_id &`audit_event`.event_time>=@start_time & `audit_event`.event_time<=@end_time;";

            var conn = new MySqlConnection(connectionString);

            var queryParameterList = new List<MySqlParameter>
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) {Value = queryUserId.Value},
                new MySqlParameter("@start_time",MySqlDbType.Datetime) {Value = queryStartDate},
                new MySqlParameter("@end_time", MySqlDbType.Datetime) {Value = queryEndDate},
            };

            var result = await _mysqlConnector.GetDataTableAsync(conn, sql, queryParameterList.ToArray());

            var auditEventList = new List<AuditEvent>();

            foreach (System.Data.DataRow row in result.Rows)
            {
                auditEventList.Add(new AuditEvent
                {
                    Id = Convert.ToInt32(row["id"].ToString()),
                    EventName = row["event_name"].ToString(),
                    EventUniqueId = row["event_unique_id"].ToString(),
                    EventSource = row["event_source"].ToString(),
                    EventTime = Convert.ToDateTime(row["event_time"].ToString()),
                    RequestOrigin = row["request_origin"].ToString(),
                    RequestAction = row["request_action"].ToString(),
                    RequestHeaders = row["request_headers"].ToString(),
                    RequestSource = row["request_source"].ToString(),
                    UserId = Convert.ToInt32(row["user_id"].ToString()),
                    EventPayload = new Domain.Model.AuditEventPayload
                    {
                        EventId = Convert.ToInt32(row["id"].ToString()),
                        RequestPayLoadType = row["request_payload_type"].ToString(),
                        RequestPayLoadContent = row["request_payload_content"].ToString()
                    }
                });
            }

            return auditEventList;
        }

        public async Task<AuditEvent> GetLastRecordAsync()
        {
            var connectionString = Constants.SqlConn;
            var sql = "select audit_event.*, audit_event_payload.* from `audit_event`  " +
                      "inner join `audit_event_payload` on audit_event_payload.event_id=`audit_event`.id " +
                      "order by audit_event.id desc limit 1";

            var conn = new MySqlConnection(connectionString);

            var result = await _mysqlConnector.GetDataTableAsync(conn, sql);

            return new AuditEvent
            {
                Id = Convert.ToInt32(result.Rows[0]["id"].ToString()),
                EventName = result.Rows[0]["event_name"].ToString(),
                EventUniqueId = result.Rows[0]["event_unique_id"].ToString(),
                EventSource = result.Rows[0]["event_source"].ToString(),
                EventTime = Convert.ToDateTime(result.Rows[0]["event_time"].ToString()),
                RequestOrigin = result.Rows[0]["request_origin"].ToString(),
                RequestAction = result.Rows[0]["request_action"].ToString(),
                RequestHeaders = result.Rows[0]["request_headers"].ToString(),
                RequestSource = result.Rows[0]["request_source"].ToString(),
                UserId = Convert.ToInt32(result.Rows[0]["user_id"].ToString()),
                EventPayload = new Domain.Model.AuditEventPayload
                {
                    EventId = Convert.ToInt32(result.Rows[0]["id"].ToString()),
                    RequestPayLoadType = result.Rows[0]["request_payload_type"].ToString(),
                    RequestPayLoadContent = result.Rows[0]["request_payload_content"].ToString()
                }
            };
        }

        public async Task<bool> DeleteByUserIdAsync(int userId)
        {
            var connectionString = Constants.SqlConn;
            var sql = "SET FOREIGN_KEY_CHECKS=0; " +
                      "delete audit_event, audit_event_payload from audit_event " +
                      "left join audit_event_payload on audit_event_payload.event_id = audit_event.id " +
                      "where audit_event.user_id = @user_id; " +
                      "SET FOREIGN_KEY_CHECKS = 1;";

            var conn = new MySqlConnection(connectionString);
            var queryParameterList = new List<MySqlParameter>
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) {Value = userId}
                
            };

            var result = await _mysqlConnector.ExecuteNonQueryAsync(conn, sql, queryParameterList.ToArray());

            if (!result)
                throw new RepositoryException("Cannot Delete Records");

            return result;
        }
    }
}