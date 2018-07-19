using System;
using App.Audit.Domain.Command;
using App.Audit.Domain.Model;
using App.Audit.Infrastructure.Validator;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Audit.UnitTest.Validators
{
    [TestClass]
    public class AuditEventCreateCommandValidatorTest
    {


        [TestMethod]
        public void Validate_BaseSuccess()
        {

            var eventPayloadFake = Builder<AuditEventPayload>.CreateNew().With(r => r.EventId = null).Build();
            var auditModelFake = Builder<AuditEvent>.CreateNew().With(r=>r.EventPayload=eventPayloadFake).Build();
            var auditEventCreateCommandFake = Builder<AuditEventCreateCommand>.CreateNew()
                .With(r => r.Id = Guid.NewGuid()).With(r => r.Model = auditModelFake).Build();


            var validator = new AuditEventCreateCommandValidator();
            var result = validator.Validate(auditEventCreateCommandFake);
            Assert.IsTrue(result.IsValid);

        }



        [TestMethod]
        public void Validate_IdNotEmtpy()
        {

            var eventPayloadFake = Builder<AuditEventPayload>.CreateNew().With(r => r.EventId = null).Build();
            var auditModelFake = Builder<AuditEvent>.CreateNew().With(r => r.EventPayload = eventPayloadFake).Build();
            var auditEventCreateCommandFake = Builder<AuditEventCreateCommand>.CreateNew()
                .With(r => r.Id = Guid.Empty).With(r => r.Model = auditModelFake).Build();


            var validator = new AuditEventCreateCommandValidator();
            var result = validator.Validate(auditEventCreateCommandFake);
            Assert.IsFalse(result.IsValid);

        }
    }
}
