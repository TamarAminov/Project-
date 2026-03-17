using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.TasksDto
{
    public class TasksDtoo
    {
        public int TaskID { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateOnly DueDate { get; set; }
        public int? VendorID { get; set; }
        public VendorDto.VendorDtoo? AllVendor { get; set; }
        public int  EventID { get; set; }
        //public  EventDto.EventDtoo AllEvent { get; set; }
       
    }
    public record GenerateTasksDto(int EventID, int VendorID);
    public record ToggleTaskDto(bool IsCompleted);
}
