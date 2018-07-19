using System;
using App.Audit.Domain.Model;
using App.Common.Interface;

namespace App.Audit.Domain.Command
{
    public class AuditEventCreateCommand: ICommand
    {
        public Guid Id { get; set; }
        public AuditEvent Model { get; set; }
    }
}