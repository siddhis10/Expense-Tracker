using Expense_Tracker.Models;
using Expense_Tracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext context;

        public TransactionController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Transactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
           
        }

        public IActionResult Upsert(int id=0)
        {
            GetDropdown();
            if (id == 0)
            {
                return View(new Transaction());
            }
            else
            {
                return View(context.Transactions.Find(id));
            }
            
        }

        public void GetDropdown()
        {
            var CategoryCollection=context.Categories.ToList();
            Category DefaultCategory=new Category() { CategoryId = 0, Title = "Select Category" };
            CategoryCollection.Insert(0,DefaultCategory);
            ViewBag.Category = CategoryCollection;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert([Bind("TransactionId,CategoryId,Amount,Note,CreatedAt")] Transaction transaction)
        {
            if (ModelState.IsValid)
            { 
                if(transaction.TransactionId == 0)
                {
                    context.Add(transaction);
                }
                else
                {
                    context.Update(transaction);
                }
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            GetDropdown();
            return View(transaction);
        }


        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var obj = context.Transactions.FirstOrDefault(x => x.TransactionId == id);
            if (obj != null)
            {
                context.Transactions.Remove(obj);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);
        }
    }
}
