using App.Audit.Domain.Model;
using App.Audit.Infrastructure.Repository;
using App.Common.Utility;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace App.Audit.IntegrationTest
{
    [TestClass]
    public class AuditRepositoryTest
    {
        [TestMethod]
        public void AddAsync_VerifyLastInsert()
        {
            var mysqlConnector = new MysqlConnector();
            var auditRepo = new AuditRepository(mysqlConnector);

            //Build fake
            var auditPayloadFake = Builder<AuditEventPayload>.CreateNew().Build();
            var auditEventFake = Builder<AuditEvent>.CreateNew().With(r => r.EventPayload = auditPayloadFake).Build();

            //Add Record
            var addresult = auditRepo.AddAsync(auditEventFake).Result;

            //Get Last Record
            var lastRecord = auditRepo.GetLastRecordAsync().Result;

            //Verify not null
            Assert.IsNotNull(lastRecord);
            Assert.AreEqual(auditEventFake.EventName, lastRecord.EventName);
            //TODO: More Validation
        }

        [TestMethod]
        public void GetAuditLogsAsync_verifyonlyOne()
        {
            var mysqlConnector = new MysqlConnector();
            var auditRepo = new AuditRepository(mysqlConnector);

            var userId = 10;

            var fromDate = DateTime.Today.AddDays(-1);
            var toDate = DateTime.Today.AddDays(1);

            var auditPayloadFake = Builder<AuditEventPayload>.CreateNew().Build();
            var auditEventFake = Builder<AuditEvent>.CreateNew().With(r => r.EventPayload = auditPayloadFake)
                    .With(r => r.UserId = userId)
                .With(r => r.EventTime = DateTime.Today)
                .Build();

            //Delete all record with same user id
            var deleteresult = auditRepo.DeleteByUserIdAsync(userId).Result;

            //Add fake record
            var addresult = auditRepo.AddAsync(auditEventFake).Result;

            //Get record with same UserId and Dates and it must be that one only
            var onlyRecord = auditRepo.GetAuditLogsAsync(userId, fromDate, toDate).Result;

            Assert.IsNotNull(onlyRecord);
            Assert.IsTrue(onlyRecord.Count == 1);
            Assert.IsNotNull(onlyRecord[0]);
            Assert.AreEqual(auditEventFake.EventName, onlyRecord[0].EventName);
            Assert.AreEqual(auditEventFake.UserId, onlyRecord[0].UserId);
            Assert.AreEqual(auditEventFake.EventTime, onlyRecord[0].EventTime);
            //TODO: More Validation
        }
    }
}