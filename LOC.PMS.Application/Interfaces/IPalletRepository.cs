using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IPalletRepository
    {
       Task InsertPallet(PalletDetailsRequest palletDetailsRequest);
    }
}