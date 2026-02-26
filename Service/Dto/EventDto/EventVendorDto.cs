using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.EventDto
{
    public class EventVendorDto
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int UserID { get; set; }
        public UserDto.UserUpdateDto AllUser { get; set; }

        public string EventTypeID { get; set; }
        public EventTypeDto.EventTypeDtoo AllEventType { get; set; }
        //public int TotalBudget { get; set; }
        //public int GuestCount { get; set; }
    }
}
