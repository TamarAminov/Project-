using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.TasksDto
{
    public class TaskChangeCompleteDto
    {
        public int TaskID { get; set; }
        public bool IsCompleted { get; set; }

    }
}
