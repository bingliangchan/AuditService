using System.Threading.Tasks;
using App.Audit.Domain.Query;
using App.Audit.Infrastructure.Repository;
using App.Common.Interface;

namespace App.Audit.Infrastructure.QueryHandler
{
    public class GetAuditEventQueryHandler: IQueryHandler<GetAuditEventQuery, GetAuditEventQueryResult>
    {
        private readonly IAuditRepository _auditRepository;

        public GetAuditEventQueryHandler(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task<GetAuditEventQueryResult> Execute(GetAuditEventQuery query)
        {
            var startDate = new System.DateTime(query.StartDate.Value.Year, query.StartDate.Value.Month, query.StartDate.Value.Day);
            var endDate = new System.DateTime(query.EndDate.Value.Year, query.EndDate.Value.Month, query.EndDate.Value.Day,23,59,59);

            var result = await _auditRepository.GetAuditLogsAsync(query.UserId, startDate, endDate);
            return new GetAuditEventQueryResult {AuditEvents = result};

        }
    }
}