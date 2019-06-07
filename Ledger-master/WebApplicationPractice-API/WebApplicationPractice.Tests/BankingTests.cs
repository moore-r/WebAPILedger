using NUnit.Framework;
using WebApplicationPractice.Ledger;

namespace Tests
{
    // This class just has some simple unit tests for the banking class
    public class Tests
    {
        [Test]
        public void InitialBalanceTest()
        {
            var _banking = new Banking();
            Assert.AreEqual(_banking.balance, 50);
        }

        [Test]
        public void WithdrawalTest()
        {
            var _banking = new Banking();

            // Value will stay the same - balance cant drop below 0
            _banking.RecordWithdrawal(-65);
            Assert.AreEqual(_banking.balance, 50);

            _banking.RecordWithdrawal(-10);
            Assert.AreEqual(_banking.balance, 40);

            // Make sure balance can hit 0
            _banking.RecordWithdrawal(-40);
            Assert.AreEqual(_banking.balance, 0);
        }

        [Test]
        public void DepositTest()
        {
            var _banking = new Banking();

            _banking.RecordDeposit(15);
            Assert.AreEqual(_banking.balance, 65);
        }

        [Test]
        public void HistoryTest()
        {
            var _banking = new Banking();

            _banking.RecordDeposit(15);

            Assert.AreEqual(_banking.history[0].NewBankBalance, 65);
            Assert.AreEqual(_banking.history[0].OldBankBalance, 50);
            Assert.AreEqual(_banking.history[0].Change, 15);    
        }
    }
}