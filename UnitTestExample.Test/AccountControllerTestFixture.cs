using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    class AccountControllerTestFixture
    {
        [Test,
         TestCase("abcd1234", false),
         TestCase("irf@uni-corvinus", false),
         TestCase("irf.uni-corvinus.hu", false),
         TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();
            var actualResult = accountController.ValidateEmail(email);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test,
         TestCase("asfdasfd", false),
         TestCase("SADF1234", false),
         TestCase("asdf1234", false),
         TestCase("ASdf1234", true)
        ]
        public void TestValidatePassword(string pass, bool expectedResult)
        {
            var accountController = new AccountController();
            var actualResult = accountController.ValidatePassword(pass);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
