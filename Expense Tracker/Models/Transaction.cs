﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key] 
        public int TransactionId { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int Amount { get; set; }
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
            {
                get
                {
                    return Category == null ? "" : Category.Icon + " " + Category.Title;
                }
        }

        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Type=="Expense") ? "-" :  " + ") + Amount.ToString("0.##") + " INR";
            }
        }
    }
}
