using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class VendorAttribute
    {
        [Key]
        public int VendorAttributeID { get; set; }
        public string VendorAttributeName { get; set; }   // שם המאפיין (לדוגמה: "PricePerDish")
        public string Value { get; set; } // הערך (לדוגמה: "250")
        public int VendorID { get; set; }
        [ForeignKey("VendorID")]
        public virtual Vendor AllVendor { get; set; }
    }
}
