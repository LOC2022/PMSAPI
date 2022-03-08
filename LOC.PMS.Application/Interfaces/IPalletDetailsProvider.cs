using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IPalletDetailsProvider
    {
        Task AddPallet(PalletDetailsRequest palletDetailsRequest);
    }
}