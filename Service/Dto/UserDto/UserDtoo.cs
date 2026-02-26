using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.UserDto
{
    public class UserDtoo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public User.EnumRole Role { get; set; }
        public ICollection<EventDto.EventDtoo>? UserEvents { get; set; }
    }
}
