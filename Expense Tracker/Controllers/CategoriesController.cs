using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;
using Expense_Tracker.Models.ViewModels;

namespace Expense_Tracker.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var data = context.Categories.ToList();
            ViewBag.datasource = data;
            return View(data);
        }



        public IActionResult Create(int? id)
        {
            CategoriesVM CategoriesVM = GetDropdown();
            if (id == 0 || id == null)
            {
                return View(CategoriesVM);
            }
            else
            {
                CategoriesVM.Category = context.Categories.FirstOrDefault(x => x.CategoryId == id);
                return View(CategoriesVM);
            }
            

        }


        public CategoriesVM GetDropdown()
        {
            CategoriesVM CategoriesVM = new()
            {
                Category = new(),
                

            };
            return CategoriesVM;
        }



        [HttpPost]
        public IActionResult Create(CategoriesVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Category.CategoryId == 0)
                {
                    context.Categories.Add(obj.Category);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                else
                {
                    context.Update(obj.Category);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            //var Departmentd = GetDropdown();
            //obj.DepartmentList = Departmentd.DepartmentList;
            return View(obj);
        }


        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var obj = context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (obj != null)
            {
                context.Categories.Remove(obj);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);
        }

    }
}
