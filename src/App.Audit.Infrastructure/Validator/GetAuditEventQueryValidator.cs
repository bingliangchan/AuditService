using App.Audit.Domain.Command;
using App.Audit.Domain.Query;
using App.Common.Validator;
using FluentValidation;

namespace App.Audit.Infrastructure.Validator
{
    public class GetAuditEventQueryValidator : MessageValidator<GetAuditEventQuery>
    {
        public GetAuditEventQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
        }
    }
}