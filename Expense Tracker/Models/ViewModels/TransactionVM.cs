using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expense_Tracker.Models.ViewModels
{
    public class TransactionVM
    {
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        public Transaction?  Transaction { get; set; }
        public Category? Category { get; set; }
    }
}
