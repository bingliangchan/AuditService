using App.Audit.Domain.Command;
using App.Common.Validator;
using FluentValidation;

namespace App.Audit.Infrastructure.Validator
{
    public class AuditEventCreateCommandValidator: MessageValidator<AuditEventCreateCommand>
    {
        public AuditEventCreateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Model).NotNull();
            RuleFor(x => x.Model.UserId).NotNull().GreaterThan(0);
            RuleFor(x => x.Model.EventName).NotNull();
            RuleFor(x => x.Model.EventUniqueId).NotNull();
            RuleFor(x => x.Model.EventSource).NotNull();
            RuleFor(x => x.Model.EventTime).NotNull();
            RuleFor(x => x.Model.RequestOrigin).NotNull();
            RuleFor(x => x.Model.RequestAction).NotNull();
            RuleFor(x => x.Model.RequestSource).NotNull();
            RuleFor(x => x.Model.UserId).NotNull();
            RuleFor(x => x.Model.EventPayload).NotNull();
            RuleFor(x => x.Model.EventPayload.EventId).Empty();
           
            //TODO: Complete Validation

        }
    }
}