using System;

namespace Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание и использование банковских счетов

            // Создание обычного банковского счёта
            {
                var account = new BankAccount("<lr1>", 1000);
                Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");
                account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
                Console.WriteLine(account.Balance);
                account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
                Console.WriteLine(account.Balance);
                // Создание и использование счёта подарочной карты
                var giftCard = new GiftCardAccount("gift card", 100, 50);
                giftCard.MakeWithdrawal(20, DateTime.Now, "get expensive coffee");
                giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");
                giftCard.PerformMonthEndTransactions();
                giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending money");
                Console.WriteLine(giftCard.GetAccountHistory());
                // Создание и использование сберегательного счёта
                var savings = new InterestEarningAccount("savings account", 10000);
                savings.MakeDeposit(750, DateTime.Now, "save some money");
                savings.MakeDeposit(1250, DateTime.Now, "Add more savings");
                savings.MakeWithdrawal(250, DateTime.Now, "Needed to pay monthly bills");
                savings.PerformMonthEndTransactions();
                Console.WriteLine(savings.GetAccountHistory());
                // Создание и использование кредитного счёта
                var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
                // How much is too much to borrow?
                lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "Take out monthly advance");
                lineOfCredit.MakeDeposit(50m, DateTime.Now, "Pay back small amount");
                lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "Emergency funds for repairs");
                lineOfCredit.MakeDeposit(150m, DateTime.Now, "Partial restoration on repairs");
                lineOfCredit.PerformMonthEndTransactions();
                Console.WriteLine(lineOfCredit.GetAccountHistory());
                // Обработка исключений при попытке перерасхода средств на обычном счёте
                try
                {
                    account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Exception caught trying to overdraw");
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(account.GetAccountHistory());
                }
            }
            // Обработка исключения при создании счёта с отрицательным балансом
            BankAccount invalidAccount;
            try
            {
                invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(e.ToString());
                return;
            }
        }
    }
}
