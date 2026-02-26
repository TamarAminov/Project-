using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Area
    {
        [Key]
        public int AreaID   { get; set; }
        public string AreaName { get; set; }
    }
}
