using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key] 
        public int TransactionId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int Amount { get; set; }
        public string? Note { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
