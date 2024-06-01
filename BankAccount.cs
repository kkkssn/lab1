using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    // Класс для работы с банковским счётом
    public class BankAccount
    {
        // Статическое поле для генерации номера счёта
        private static int s_accountNumberSeed = 1234567890;

        // Свойство для хранения номера счёта
        public string Number { get; }

        // Свойство для хранения владельца счёта
        public string Owner { get; set; }

        // Свойство для получения баланса счёта
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                // Суммирование всех транзакций для расчёта баланса
                foreach (var item in _allTransactions)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }

        // Минимальный баланс счёта
        private readonly decimal _minimumBalance;

        // Конструктор для создания экземпляра счёта с указанием имени, начального баланса и минимального баланса
        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }

        // Конструктор для создания экземпляра счёта с указанием имени, начального баланса и минимального баланса
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            // Генерация номера счёта и увеличение счётчика
            Number = s_accountNumberSeed.ToString();
            s_accountNumberSeed++;

            // Установка имени владельца счёта и минимального баланса
            Owner = name;
            _minimumBalance = minimumBalance;

            // Если начальный баланс больше нуля, добавляем его в качестве первой транзакции
            if (initialBalance > 0)
                MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        // Метод для внесения депозита на счёт
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            // Проверка на положительность суммы депозита
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            // Создание транзакции депозита и добавление её в список транзакций
            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

        // Метод для снятия средств со счёта
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            // Проверка на положительность суммы снятия
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            // Проверка на наличие достаточного баланса и создание транзакции снятия
            Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
            Transaction? withdrawal = new(-amount, date, note);
            _allTransactions.Add(withdrawal);
            // Добавление транзакции по превышению лимита, если такая имеется
            if (overdraftTransaction != null)
                _allTransactions.Add(overdraftTransaction);
        }

        // Виртуальный метод для проверки наличия средств на счёте
        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            // Если недостаточно средств, выбрасывается исключение
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            else
            {
                return default;
            }
        }

        // Список всех транзакций
        private List<Transaction> _allTransactions = new List<Transaction>();

        // Метод для получения истории транзакций
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            // Формирование отчёта о транзакциях
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }
            return report.ToString();
        }
        // Виртуальный метод для выполнения транзакций в конце месяца
        public virtual void PerformMonthEndTransactions() { }
    }
}
