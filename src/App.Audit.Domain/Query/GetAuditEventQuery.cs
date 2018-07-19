using System;
using App.Common.Interface;

namespace App.Audit.Domain.Query
{
    public class GetAuditEventQuery: IQuery
    {
        public int? UserId  { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}