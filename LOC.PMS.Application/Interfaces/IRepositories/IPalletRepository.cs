using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IPalletRepository
    {
       Task InsertPallets(PalletDetailsRequest palletDetailsRequest);
        Task AddDayPlanData(List<DayPlan> order);
    }
}