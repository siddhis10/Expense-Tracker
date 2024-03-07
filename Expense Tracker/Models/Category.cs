using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Icon { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; }

        [NotMapped]
        public string? CategorywithIcon {
            get
            {
                return this.Icon+" "+this.Title;
            }
        }
    }
}
