using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public partial class TblUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        
        public string? Username { get; set; }

        [Required]
        [RegularExpression(@"[\w._-]+@[a-z]+\.[a-z]{2,3}", ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"[89](\d){9}", ErrorMessage ="Should Start with 8 or 9")]
        public string? ContactNo { get; set; }

        //[Required(ErrorMessage ="Password is required")]
        
        
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z])(?=.*?[#?!@$%^&*-]).{8,}", ErrorMessage = "Password must be atleast 8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        
        public string? Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="Passw0rd and Confirmed password must match")]
        [NotMapped]
        public string? ConfirmPassword { get; set; }
    }
}
