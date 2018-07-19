using App.Common.Interface;
using System.Collections.Generic;
using App.Audit.Domain.Model;

namespace App.Audit.Domain.Query
{
    public class GetAuditEventQueryResult : IQueryResult
    {
        public IEnumerable<AuditEvent> AuditEvents { get; set; }
    }
}