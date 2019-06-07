using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationPractice.Accounts;

namespace WebApplicationPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public Accounts.Users AllUsers;

        public HomeController(Accounts.Users accounts)
        {
            this.AllUsers = accounts;
        }

        /// <summary>
        /// Create an account - the password can be anything, but the username cannot have any special characters. Example Input: ["username", "password"]
        /// Json Format: ["username_with_no_special_characters", "Password_were_anything_goes"]
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("CreateAccount")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult CreateAccount([FromBody] List<string> body)
        {
            try
            {
                if (AllUsers.ValidateAccountInfo(body))
                {
                    AllUsers.CreateNewAccount(body[0], body[1]);
                    return Ok("Account Created");
                }
                else
                {
                    return StatusCode(500, "User name or password is invalid.");
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error encountered when creating account. {e}");
            }
        }

        /// <summary>
        /// Login to an already exsisting account - input follows the same rules as creating an account - Example Input: ["username", "password"]
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut("login")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult login([FromBody] List<string> body)
        {
            try
            {
                if (AllUsers.ValidateAccountInfo(body))
                {
                    if (AllUsers.login(body[0], body[1]))
                        return Ok();
                    else
                        return StatusCode(500, "Username/Password combo not found.");
                }
                else
                {
                    return StatusCode(500, "User name or password is invalid.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Username/Password combo not found. {e}");
            }
        }

        /// <summary>
        /// Get the currently logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpGet("CurrentUser")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult CurrentUser()
        {
            try
            {
                string user = AllUsers.GetCurrentUser();
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error encountered when retrieving current user. {e}");
            }
        }

        /// <summary>
        /// Log out - change the current user to null.
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult logout()
        {
            try
            {
                AllUsers.logout();
                return Ok("Successfully logged out");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error encountered when logging out. {e}");
            }
        }

        /// <summary>
        /// Get the bank balance of the current user.
        /// </summary>
        /// <returns>Bank balance</returns>
        [HttpGet("Balance")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult Balance()
        {
            try
            {
                int CurrentBalance = AllUsers.CurrentUser.BankAccount.balance;
                return Ok(CurrentBalance);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error encountered when retrieving balance. {e}");
            }
        }

        /// <summary>
        /// Deposit money into the bank account of the current user. example input: 25
        /// </summary>
        /// <param name="ammount"> has to be an integer</param>
        /// <returns></returns>
        [HttpPatch("DepositMoney")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult DepositMoney([FromBody] int ammount)
        {
            try
            {
                AllUsers.CurrentUser.BankAccount.RecordDeposit(ammount);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error encountered when depositing. {e}");
            }
        }

        /// <summary>
        /// Withdraw money from the current users account, Example input: 25
        /// </summary>
        /// <param name="ammount"></param>
        /// <returns></returns>
        [HttpPatch("WithdrawMoney")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult WithdrawMoney([FromBody] int ammount)
        {
            try
            {
                // Make the withdrawal ammount a negative number.
                if (AllUsers.CurrentUser.BankAccount.RecordWithdrawal((ammount * -1)))
                {
                    return Ok();
                }

                return Ok("Withdrawal denied: bank balance not great enough for withdrawal ammount - balance can not go negative.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error encountered when withdrawing money. {e}");
            }
        }

        /// <summary>
        /// Get the transaction history for the current user.
        /// </summary>
        /// <returns></returns>
        [HttpGet("History")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult History()
        {
            try
            {
                List<Ledger.TransactionHistoryModel> UserHistory = AllUsers.CurrentUser.BankAccount.GetHistory();

                return Json(UserHistory);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error encountered when retrieving banking history. {e}");
            }
        }
    }
}
