using App.Common;

namespace App.Audit.Domain.Model
{
    public class AuditEventPayload:BaseEntity
    {
        public string RequestPayLoadType { get; set; }
        public string RequestPayLoadContent { get; set; }
        public int? EventId { get; set; }

    }
}