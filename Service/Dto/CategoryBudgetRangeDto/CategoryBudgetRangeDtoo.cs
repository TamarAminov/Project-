using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.CategoryBudgetRangeDto
{
    public class CategoryBudgetRangeDtoo
    {
        public int CategoryBudgetRangeID { get; set; }
        public int CategoryID { get; set; }
        public double MinBudget { get; set; }
        public double MaxBudget { get; set; }  // 0 = אינסוף
        public double Percentage { get; set; }
    }
}
