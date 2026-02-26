using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Vendor
    {
        [Key]
        public int VendorID { get; set; }
        public string BusinessName { get; set; }
        public bool Status {  get; set; }
        public int CategoryID { get; set; } // Catering, Photography...
        [ForeignKey("CategoryID")]
        public virtual Category AllCategory { get; set; }
       
        public int AreaID { get; set; } // צפון, מרכז, דרום
        [ForeignKey("AreaID")]
        public virtual Area AllArea { get; set; }
        public decimal BasePrice { get; set; } // מחיר התחלתי להשוואה

            // רשימת המאפיינים הייחודיים (מחיר מנה, כמות תמונות וכו')
        public ICollection<VendorAttribute> Attributes { get; set; }=new List<VendorAttribute>();
        public ICollection<Event>? Events { get; set; } =new List<Event>();
    }
    
}
