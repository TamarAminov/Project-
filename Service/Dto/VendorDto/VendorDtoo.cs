using Service.Dto.EventDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.VendorDto
{
    public class VendorDtoo
    {
        public int VendorID { get; set; }
        public string BusinessName { get; set; }
        public int CategoryID { get; set; } // Catering, Photography...
        public CategoryDto.CategoryDtoo? Category { get; set; } // Catering, Photography...
        public int AreaId { get; set; }
        public AreaDto.AreaDtoo? Area { get; set; } // צפון, מרכז, דרום
        public decimal BasePrice { get; set; } // מחיר התחלתי להשוואה
        public ICollection<EventVendorDto>? Events { get; set; }
    }
}
