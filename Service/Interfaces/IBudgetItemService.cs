using Service.Dto.BudgetItemDto;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBudgetItemService: IService<BudgetItemDtoo>
    {
        public Task<BudgetItemDtoo> ChangeLock(int id);
        public Task<BudgetItemDtoo> ChangeIngore(int id);

    }
}
