using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    // Класс для работы с банковским счетом подарочной карты
    public class GiftCardAccount : BankAccount
    {
        // Поле для хранения ежемесячного депозита
        private readonly decimal _monthlyDeposit = 0m;

        // Конструктор для создания экземпляра счёта подарочной карты с указанием имени, начального баланса и ежемесячного депозита
        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
            => _monthlyDeposit = monthlyDeposit;

        // Переопределённый метод для выполнения транзакций в конце месяца
        public override void PerformMonthEndTransactions()
        {
            // Если указан ежемесячный депозит, выполняется депозит с указанным значением
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }
    }
}

