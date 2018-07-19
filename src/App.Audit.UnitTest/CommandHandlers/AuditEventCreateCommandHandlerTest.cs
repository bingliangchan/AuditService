using App.Audit.Domain.Command;
using App.Audit.Domain.Model;
using App.Audit.Infrastructure.CommandHandler;
using App.Audit.Infrastructure.Repository;
using App.TestUtil;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace App.Audit.UnitTest.CommandHandlers
{
    [TestClass]
    public class AuditEventCreateCommandHandlerTest : AutoMockFixture<AuditEventCreateCommandHandler>
    {
        [TestMethod]
        public void Execute_VerifyRepoistoryCalled()
        {
            //Arrange
            var fakeEvent = Builder<AuditEvent>.CreateNew().Build();
            var fakeCommand = Builder<AuditEventCreateCommand>.CreateNew().With(r => r.Model = fakeEvent).Build();
            var repoResultFake = true;

            MockOf<IAuditRepository>().Setup(r => r.AddAsync(fakeEvent)).Returns(Task.FromResult(repoResultFake));

            //Act
            Sut.Execute(fakeCommand);

            //Assert
            MockOf<IAuditRepository>().Verify(r => r.AddAsync(It.IsAny<AuditEvent>()), Times.Once);
        }

        //TODO: Repository Exception Case
    }
}