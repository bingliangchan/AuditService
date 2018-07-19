using System;
using App.Common;

namespace App.Audit.Domain.Model
{
    public class AuditEvent : BaseEntity
    {
        public string EventName { get; set; }
        public string EventUniqueId { get; set; }
        public string EventSource { get; set; }
        public DateTime EventTime { get; set; }
        public string RequestOrigin { get; set; }
        public string RequestAction { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestSource { get; set; }
        public int? UserId { get; set; }
        public AuditEventPayload EventPayload { get; set; }
    }
}