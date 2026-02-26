using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.VendorAttributeDto
{
    public class VendorAttributeChangeDto
    {
        public int VendorAttributeID { get; set; }

        public string VendorAttributeName { get; set; }   // שם המאפיין (לדוגמה: "PricePerDish")
        public string Value { get; set; } // הערך (לדוגמה: "250")

    }
}
