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
                .Include(x=>x.Category).Where(y=>y.CreatedAt>=StartDate && y.CreatedAt <= EndDate).ToListAsync();

            int TotalIncome = SelectTransaction.Where(i => i.Category.Type == "Income").Sum(j => j.Amount);

            ViewBag.TotalIncome = TotalIncome.ToString("C0", CultureInfo.GetCultureInfo("en-IN"));

            int TotalExpense = SelectTransaction.Where(i => i.Category.Type == "Expense").Sum(j => j.Amount);

            ViewBag.TotalExpense = TotalExpense.ToString("C0", CultureInfo.GetCultureInfo("en-IN"));

            int Balance= TotalIncome-TotalExpense;
            ViewBag.Balance = Balance.ToString("C0", CultureInfo.GetCultureInfo("en-IN"));


            ViewBag.ChartData = SelectTransaction.Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    CategorywithIcon = k.First().Category.Icon+" "+k.First().Category.Title,
                    Amount = k.Sum(j => j.Amount),
                    FormattedAmount=k.Sum(j => j.Amount).ToString("C0", CultureInfo.GetCultureInfo("en-IN"))
                }).
                ToList();


                List<splinechartdata>IncomeSummary =SelectTransaction
                .Where(i=>i.Category.Type=="Income").GroupBy(j=>j.CreatedAt)
                .Select(k=> new splinechartdata()
                {
                    day=k.First().CreatedAt.ToString("dd-MMM"),
                    Income=k.Sum(j => j.Amount)
                }).ToList();

            List<splinechartdata> ExpenseSummary = SelectTransaction
            .Where(i => i.Category.Type == "Expense").GroupBy(j => j.CreatedAt)
            .Select(k => new splinechartdata()
            {
                day = k.First().CreatedAt.ToString("dd-MMM"),
                Expense = k.Sum(j => j.Amount)
            }).ToList();


            string[] Last7days =Enumerable.Range(0,7).Select(i=>StartDate.AddDays(i).ToString("dd-MMM")).ToArray();
            ViewBag.SplineChartData = from day in Last7days
                                      join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.Income,
                                          expense = expense == null ? 0 : expense.Expense,
                                      };


            return View();
        }

        public class splinechartdata
        {
            public string day;
            public int Income;
            public int Expense;
        }
    }
}
