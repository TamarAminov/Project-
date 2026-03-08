using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class BudgetItem
    {
        [Key]
        public int BudgetItemID {  get; set; }
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public virtual Event AllEvent { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category AllCategory { get; set; }
        public int PlannedAmount { get; set; }
        public int ActualAmount { get; set; }
        public bool IsIgnore { get; set; }
        public bool IsLocked { get; set; }
        public int? VendorID { get; set; }
        public Vendor? Vendor { get; set; }
    }
}
