using NUnit.Framework;
using System.Collections.Generic;
using WebApplicationPractice.Accounts;

namespace WebApplicationPractice.Tests.Account
{
    class UsersTests
    {
        [Test]
        public void CreateNewAccountTest()
        {
            var _account = new Users();

            _account.CreateNewAccount("TestUser", "TestPass");
            Assert.AreEqual(_account.AccountList[0].UserName, "TestUser");
        }

        [Test]
        public void LoginLogoutTest()
        {
            var _account = new Users();

            _account.CreateNewAccount("TestUser", "TestPass");
            Assert.False(_account.login("TestUse", "TestPass"));
            Assert.True(_account.login("TestUser", "TestPass"));

            Assert.AreEqual(_account.GetCurrentUser(), "TestUser");
        }

        [Test]
        public void ValidateAccountInfo()
        {
            var _account = new Users();


            List<string> InvalidInfo = new List<string>();
            InvalidInfo.Add("username!");
            InvalidInfo.Add("password");

            List<string> InvalidInfo2 = new List<string>();
            InvalidInfo.Add("user-name");
            InvalidInfo.Add("password");


            // Check for invlaid characters
            Assert.False(_account.ValidateAccountInfo(InvalidInfo));
            Assert.False(_account.ValidateAccountInfo(InvalidInfo2));
        }
    }
}
