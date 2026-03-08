using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public  class CategoryBudgetRange
    {
        [Key]
        public int CategoryBudgetRangeID { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public double MinBudget { get; set; }
        public double MaxBudget { get; set; }  // 0 = אינסוף
        public double Percentage { get; set; }

        // Navigation Property
        public EventType Category { get; set; }
    }
}
