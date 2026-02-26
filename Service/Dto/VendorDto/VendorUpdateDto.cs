using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.VendorDto
{
    public class VendorUpdateDto
    {
        public int VendorID { get; set; }
        public string BusinessName { get; set; }
        public int AreaId { get; set; }
        public AreaDto.AreaDtoo Area { get; set; } // צפון, מרכז, דרום
        public decimal BasePrice { get; set; } // מחיר התחלתי להשוואה
    }
}
