using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.EventDto
{
    public class EventDtoo
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int UserID { get; set; }
        //public UserDto.UserDtoo AllUser { get; set; }

        public int EventTypeID { get; set; }
        public EventTypeDto.EventTypeDtoo? AllEventType { get; set; }
        public int TotalBudget { get; set; }
        public int GuestCount { get; set; }
        public ICollection<TasksDto.TasksDtoo>? Tasks { get; set; }
        public ICollection<VendorDto.VendorDtoo>? Vendors { get; set; }
        public ICollection<BudgetItemDto.BudgetItemDtoo>? budgetItems { get; set; }
    }
}
