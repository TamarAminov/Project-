using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }
        
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User AllUser { get; set; }
        public int EventTypeID { get; set; }
        [ForeignKey("EventTypeID")]
        public virtual EventType AllEventType { get; set; }
        public int TotalBudget { get; set; }
        public int GuestCount { get; set; }
        public virtual ICollection<Tasks>? Tasks { get; set; }= new List<Tasks>();
        public virtual ICollection<BudgetItem>? BudgetItems { get; set; }= new List<BudgetItem>();
        public virtual ICollection<Vendor>? Vendors { get; set; }= new List<Vendor>();


    }
}
