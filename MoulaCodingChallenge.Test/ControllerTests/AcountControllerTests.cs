using Moq;
using MoulaCodingChallenge.Controllers.V1;
using MoulaCodingChallenge.Services;
using NUnit.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Security.Claims;
using MoulaCodingChallenge.Contracts;
using System.Threading.Tasks;

namespace MoulaCodingChallenge.Test.ControllerTests
{
    public  enum ACCOUNTS
    {
        EXCEPTION=1,
        OK=2,
        NOTFOUND=3
    }

    public class AcountControllerTests
    {
        private Mock<IAccountService> _mockService;
        private const string NOTFOUND = "notfound";
        private const string OK = "OK";
        private const string EXCEPTION = "exception";

        [OneTimeSetUp]
        public void setup()
        {
            Account nullresult = null;
            _mockService = new Mock<IAccountService>();
            _mockService.Setup(m => m.GetAccountAsync((int)ACCOUNTS.EXCEPTION, EXCEPTION)).Throws(new Exception());
            _mockService.Setup(m => m.GetAccountAsync((int)ACCOUNTS.OK, OK)).Returns(Task.FromResult(new Account()));
            _mockService.Setup(m => m.GetAccountAsync((int)ACCOUNTS.NOTFOUND, NOTFOUND)).Returns(Task.FromResult(nullresult));

        }

        [Test]
        public void TestExceptionFromService()
        {

            TestResponse(ACCOUNTS.EXCEPTION, EXCEPTION, StatusCodes.Status500InternalServerError);

        }

        [Test]
        public void TestOK()
        {
            TestResponse(ACCOUNTS.OK, OK, StatusCodes.Status200OK);

        }

        [Test]
        public void TestValidNotFound()
        {

            TestResponse(ACCOUNTS.NOTFOUND, NOTFOUND, StatusCodes.Status404NotFound);
        }

        private void TestResponse(ACCOUNTS account, string username, int expectedCode)
        {
            // arrange
            AccountControllerV1 controller = new AccountControllerV1(_mockService.Object);
            controller.ControllerContext.HttpContext = MockIdentity(username);
            // action
            ObjectResult result = (ObjectResult)controller.Get((int)account).Result;

            //assert
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        private HttpContext MockIdentity(string username)
        {

            Mock<ClaimsPrincipal> mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns(username);
            mockPrincipal.Setup(p => p.IsInRole("User")).Returns(true);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.User).Returns(mockPrincipal.Object);
            return mockHttpContext.Object;
        }


    }
}
