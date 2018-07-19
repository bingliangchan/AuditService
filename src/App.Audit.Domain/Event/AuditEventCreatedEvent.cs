using System;
using App.Audit.Domain.Model;
using App.Common.Interface;

namespace App.Audit.Domain.Event
{
    public class AuditEventCreatedEvent: IEvent
    {
        public Guid Id { get; set; }
        public AuditEvent Model { get; set; }
    }
}