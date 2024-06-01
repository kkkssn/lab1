using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    // Класс для представления транзакции
    public class Transaction
    {
        // Сумма транзакции
        public decimal Amount { get; }
        // Дата транзакции
        public DateTime Date { get; }
        // Примечания к транзакции
        public string Notes { get; }

        // Конструктор для создания экземпляра транзакции с указанием суммы, даты и примечаний
        public Transaction(decimal amount, DateTime date, string note)
        {
            Amount = amount;
            Date = date;
            Notes = note;
        }
    }
}


