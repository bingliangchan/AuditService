using App.Audit.Domain.Query;
using App.Audit.Infrastructure.Validator;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace App.Audit.UnitTest.Validators
{
    [TestClass]
    public class GetAuditEventQueryValidatorTest
    {
        [TestMethod]
        public void Validate_BaseSuccess()
        {
            var getAuditEventQueryFake = Builder<GetAuditEventQuery>.CreateNew()
                .With(r => r.UserId = 3)
                .With(r => r.StartDate = DateTime.Today.AddDays(-2))
                .With(r => r.EndDate = DateTime.Today).Build();

            var validator = new GetAuditEventQueryValidator();
            var result = validator.Validate(getAuditEventQueryFake);
            Assert.IsTrue(result.IsValid);
        }

        //TODO: Bad scenarios etc
    }
}