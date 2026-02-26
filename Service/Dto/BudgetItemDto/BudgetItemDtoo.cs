using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.BudgetItemDto
{
    public class BudgetItemDtoo
    {
        //public string EventName { get; set; }
        //public string CategoryName { get; set; }
        //public int PlannedAmount { get; set; }
        //public int ActualAmount { get; set; }

        //public string VendorName { get; set; }
        public int BudgetItemID { get; set; }
        public string EventID { get; set; }
       // public virtual EventDto AllEvent { get; set; }
        public int CategoryID { get; set; }
        public virtual CategoryDto.CategoryDtoo AllCategory { get; set; }

        public int PlannedAmount { get; set; }
        public int ActualAmount { get; set; }// בהתחלה 0
        public int VendorID { get; set; }
        public VendorDto.VendorDtoo AllVendor{ get; set; }
    }
}
