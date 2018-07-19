using App.Audit.Domain.Command;
using App.Common.Interface;
using System.Threading.Tasks;
using App.Audit.Infrastructure.Repository;

namespace App.Audit.Infrastructure.CommandHandler
{
    public class AuditEventCreateCommandHandler : ICommandHandler<AuditEventCreateCommand>
    {
        private readonly IAuditRepository _auditRepository;

        public AuditEventCreateCommandHandler(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task Execute(AuditEventCreateCommand command)
        {
            await _auditRepository.AddAsync(command.Model);
        }
    }
}