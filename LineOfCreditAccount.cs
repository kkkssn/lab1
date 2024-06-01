using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    // Класс для работы с кредитным счётом
    public class LineOfCreditAccount : BankAccount
    {
        // Переопределённый метод для выполнения транзакций в конце месяца
        public override void PerformMonthEndTransactions()
        {
            // Если баланс отрицательный, начисляется процент по кредиту
            if (Balance < 0)
            {
                // Рассчитываем сумму начисленного процента
                decimal interest = -Balance * 0.07m;
                // Снимаем средства в виде процентов в конце месяца
                MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
            }
        }

        // Конструктор для создания экземпляра кредитного счёта с указанием имени, начального баланса и кредитного лимита
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        {
        }

        // Переопределённый метод для проверки превышения лимита снятия
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
        isOverdrawn
        ? new Transaction(-20, DateTime.Now, "Apply overdraft fee")
        : default;
    }

}
