using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationPractice.Ledger;

namespace WebApplicationPractice.Accounts
{
    /// <summary>
    /// The useraccoutn class has the users username and password,
    /// and a islogged in flag (that I may make use of later)
    /// </summary>
    public class UserAccount
    {
        public string UserName { get; set; }
        private readonly string Password;
        public bool isLoggedIn = false;

        public Banking BankAccount;

        /// <summary>
        /// Simple constructor to instantiate the nbank account and set the user name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public UserAccount(string userName, string password)
        {
            UserName = userName;
            Password = password;

            BankAccount = new Banking();
        }

        /// <summary>
        ///  Check if the provided password matches the password
        /// </summary>
        /// <param name="FrontEndProvidedPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string FrontEndProvidedPassword)
        {
            if (FrontEndProvidedPassword.Equals(Password))
                return true;

            return false;
        }
    }
}
