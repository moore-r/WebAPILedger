using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplicationPractice.Accounts
{
    /// <summary>
    /// This class stores a list of user accounts and the currently logged in user
    /// It logs users in and out, and provides some basic validation.
    /// </summary>
    public class Users
    {
        public List<UserAccount> AccountList;
        public UserAccount CurrentUser;

        /// <summary>
        /// Simple constructor to create a new list and set the current user to null.
        /// </summary>
        public Users()
        {
            this.AccountList = new List<UserAccount>();
            CurrentUser = null;
        }

        /// <summary>
        /// Passes the username and password to the user account class to create a new account
        /// </summary>
        /// <param name="userName"> the user name</param>
        /// <param name="passWord"> the password </param>
        public void CreateNewAccount(string userName, string passWord)
        {
            UserAccount account = new UserAccount(userName, passWord);
            this.AccountList.Add(account);
        }

        /// <summary>
        /// Checks the user name and password against the list of current accounts in order
        /// to log a user in if they provide the right info
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns> true if the user is found, false if not</returns>
        public bool login(string userName, string passWord)
        {
            int index = this.AccountList.FindIndex(x => x.UserName.Equals(userName));

            if (index == -1)
                return false;

            if (this.AccountList[index].VerifyPassword(passWord))
            {
                this.AccountList[index].isLoggedIn = true;
                CurrentUser = this.AccountList[index];

                return true;                
            }

            return false;
        }

        /// <summary>
        /// Simple logout function - sets the current user to null, 
        /// as well as turning the flag off in the useraccount class.
        /// </summary>
        public void logout()
        {
            CurrentUser.isLoggedIn = false;
            CurrentUser = null;
        }

        /// <summary>
        /// Simple function to return the username of the currently logged in user
        /// Made to help test the swagger API page
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUser()
        {
            if (CurrentUser.UserName == null)
                return "No users currently logged in.";
            return CurrentUser.UserName;
        }

        /// <summary>
        /// Validate the passed in username and password - the pass word can be anything, username can have no special characters.
        /// </summary>
        /// <param name="AccountInfo"></param>
        /// <returns> true if the api was given correctly formatted data</returns>
        public bool ValidateAccountInfo(List<string> AccountInfo)
        {
            if (AccountInfo.Count != 2)
                return false;
            if (string.IsNullOrEmpty(AccountInfo[0]) || string.IsNullOrEmpty(AccountInfo[1]))
                return false;

            // Regex that checks the username for any special characters.
            var RegExForSpecialCharacters = new Regex("^[a-zA-Z0-9 ]*$");
            if (!RegExForSpecialCharacters.IsMatch(AccountInfo[0]))
                return false;

            return true;
        }
    }
}
