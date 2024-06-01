using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    // Класс для работы с банковским счетом, приносящим проценты
    public class InterestEarningAccount : BankAccount
    {
        // Конструктор для создания экземпляра счёта, приносящего проценты, с указанием имени и начального баланса
        public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
        {
        }

        // Переопределённый метод для выполнения транзакций в конце месяца
        public override void PerformMonthEndTransactions()
        {
            // Если баланс счёта больше 500, начисляются проценты
            if (Balance > 500m)
            {
                decimal interest = Balance * 0.02m; // Рассчитывается сумма процентов
                MakeDeposit(interest, DateTime.Now, "apply monthly interest"); // Проценты вносятся как депозит в конце месяца
            }
        }
    }
}

