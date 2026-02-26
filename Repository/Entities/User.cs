using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class User
    {
        public enum EnumRole
        {
            Manager,Vendor,Client
        }
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPasswordHash { get; set; }
        public string UserPhone { get; set; }
        public EnumRole Role { get; set; }
        public ICollection<Event>? UserEvents { get; set; }
    }
}
