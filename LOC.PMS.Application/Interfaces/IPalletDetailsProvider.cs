using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IPalletDetailsProvider
    {
        Task AddOrModifyPallet(PalletDetails palletDetailsRequest);
        Task<IEnumerable<PalletDetails>> GetPalletDetails(string palletPartNo);
        Task DeletePalletByPartNo(int palletId);
    }
}