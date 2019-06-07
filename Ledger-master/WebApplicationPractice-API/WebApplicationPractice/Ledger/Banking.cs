using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationPractice.Ledger
{
    /// <summary>
    /// The banking class holds the bank accounts balance and transaction history
    /// It also does withdrawals and deposits - nothing complicated.
    /// </summary>
    public class Banking
    {
        public int balance { get; set; }
        public List<TransactionHistoryModel> history = new List<TransactionHistoryModel>();

        /// <summary>
        /// Constructor - start out new banking accounts with $50
        /// </summary>
        public Banking()
        {
            balance = 50;
        }

        /// <summary>
        /// Withdraw money from the balnce - ammount turned negative in the controller
        /// Don't allow a user to go below 0.
        /// </summary>
        /// <param name="withdrawal"></param>
        public bool RecordWithdrawal(int withdrawal)
        {
            if ( 0 > (balance + withdrawal))
            {
                // Don't let users go into the negatives.
                return false;
            }
            else
            {
                balance += withdrawal;
                AddToTransactionHistory(withdrawal);
                return true;
            }
        }

        /// <summary>
        /// Deposit money onto the balance
        /// </summary>
        /// <param name="deposit"></param>
        public void RecordDeposit(int deposit)
        {
            balance += deposit;
            AddToTransactionHistory(deposit);
        }
        
        /// <summary>
        /// Save the current transaction onto the transaction history list
        /// </summary>
        /// <param name="ChangeAmmount"></param>
        public void AddToTransactionHistory(int ChangeAmmount)
        {
            TransactionHistoryModel CurrentTransaction = new TransactionHistoryModel();
            CurrentTransaction.NewBankBalance = balance;
            CurrentTransaction.OldBankBalance = (balance - ChangeAmmount);
            CurrentTransaction.Change = ChangeAmmount;

            history.Add(CurrentTransaction);
        }

        /// <summary>
        /// Get an accounts entire transaction history.
        /// </summary>
        /// <returns></returns>
        public List<TransactionHistoryModel> GetHistory()
        {
            return history;
        }
    }
}
