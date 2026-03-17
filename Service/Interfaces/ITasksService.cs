using Service.Dto.TasksDto;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITasksService : IService<TasksDtoo>
    {
        Task<TasksDtoo?> ToggleTask(int taskId, bool isCompleted);
        Task GenerateTasksForVendor(int eventId, int vendorId);
    }
}
