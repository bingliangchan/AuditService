using System.Net;
using System.Threading.Tasks;
using App.Audit.Domain.Model;
using CF.RESTClientDotNet;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Audit.FunctionTest
{
    [TestClass]
    public class AuditRepositoryTest
    {
        const string BaseUrl = "http://localhost:6559/api/";


        [TestMethod]
        public async Task Aduit_Success()
        {
            var auditPayloadFake = Builder<AuditEventPayload>.CreateNew().With(r=>r.EventId=null).Build();
            var auditEventFake = Builder<AuditEvent>.CreateNew().With(r => r.EventPayload = auditPayloadFake).Build();


            var auditServiceClient = new AuditRestClient();

            var result =await  auditServiceClient.Post(auditEventFake);

            Assert.AreEqual(result.StatusCode,HttpStatusCode.OK);

            //TODO: Not Completed

        }
    }
}
