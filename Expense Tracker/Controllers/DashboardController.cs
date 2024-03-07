using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext context;

        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ActionResult> Index()
        {

            DateTime StartDate= DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> SelectTransaction= await context.Transactions
                .Include(x=>x.Category).Where(y=>y.CreatedAt>=StartDate && y.CreatedAt >= EndDate).ToListAsync();

            int TotalIncome = SelectTransaction.Where(i => i.Category.Type == "Income").Sum(j => j.Amount);

            ViewBag.TotalIncome = TotalIncome.ToString("C0", CultureInfo.GetCultureInfo("en-IN"));

            int TotalExpense = SelectTransaction.Where(i => i.Category.Type == "Expense").Sum(j => j.Amount);

            ViewBag.TotalExpense = TotalExpense.ToString("C0", CultureInfo.GetCultureInfo("en-IN"));

            int Balance= TotalIncome-TotalExpense;
            ViewBag.Balance = Balance.ToString("C0", CultureInfo.GetCultureInfo("en-IN"));
            return View();
        }
    }
}
