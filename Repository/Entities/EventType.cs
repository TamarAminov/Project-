using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class EventType
    {
        [Key]
        public int EventTypeID {  get; set; }
        public string EventTypeName {  get; set; }
    }
}
