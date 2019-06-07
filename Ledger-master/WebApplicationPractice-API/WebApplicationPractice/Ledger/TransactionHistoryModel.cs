using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationPractice.Ledger
{
    /// <summary>
    /// This class is a model for the transanction history of a bank account
    /// Shows what the blance is after and before a transaction, also the 
    /// ammount the blance changed.
    /// </summary>
    public class TransactionHistoryModel
    {
        public int OldBankBalance { get; set; }
        public int NewBankBalance { get; set; }
        public int Change { get; set; }
    }
}
