using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.VendorAttributeDto
{
    public class VendorAttributeDtoo
    {
        public int VendorAttributeID { get; set; }

        public string VendorAttributeName { get; set; }   // שם המאפיין (לדוגמה: "PricePerDish")
        public string Value { get; set; } // הערך (לדוגמה: "250")

        public int VendorId { get; set; }
    }
}
