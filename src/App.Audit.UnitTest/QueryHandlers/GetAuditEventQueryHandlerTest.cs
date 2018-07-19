using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Audit.Domain.Model;
using App.Audit.Domain.Query;
using App.Audit.Infrastructure.QueryHandler;
using App.Audit.Infrastructure.Repository;
using App.TestUtil;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace App.Audit.UnitTest.QueryHandlers
{
    [TestClass]
    public class GetAuditEventQueryHandlerTest: AutoMockFixture<GetAuditEventQueryHandler>
    {

        [TestMethod]
        public async Task Execute_Success()
        {

            var queryFake = Builder<GetAuditEventQuery>.CreateNew().With(r=>r.StartDate=DateTime.Today.AddDays(-3)).With(r=>r.EndDate=DateTime.Today).Build();

            var auditEventListFake = new List<AuditEvent>()
            {
                Builder<AuditEvent>.CreateNew().Build(),
                Builder<AuditEvent>.CreateNew().Build(),
                Builder<AuditEvent>.CreateNew().Build(),
            };

          
            MockOf<IAuditRepository>().Setup(r =>
                r.GetAuditLogsAsync(It.IsAny<int?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).Returns(Task.FromResult(auditEventListFake));

            var result = await Sut.Execute(queryFake);


            Assert.IsNotNull(result);
            Assert.IsNotNull(result.AuditEvents);
            Assert.AreEqual(result.AuditEvents.Count(), auditEventListFake.Count);

        }

        //TODO:More scenarios on exceptions


       
    }
}
