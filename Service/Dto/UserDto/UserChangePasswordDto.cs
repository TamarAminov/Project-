using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.UserDto
{
    public class UserChangePasswordDto
    {
        //public int UserID { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail {  get; set; }
        public string UserPasswordNew { get; set; }

    }
}
