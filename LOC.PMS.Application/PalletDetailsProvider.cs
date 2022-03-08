using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;

namespace LOC.PMS.Application
{
    public class PalletDetailsProvider:IPalletDetailsProvider
    {
        private readonly IPalletRepository _palletRepository;
        public PalletDetailsProvider(IPalletRepository palletRepository)
        {
            _palletRepository = palletRepository;
        }


        public Task AddPallet(PalletDetailsRequest palletDetailsRequest)
        {
            //business logic

            _palletRepository.InsertPallet(palletDetailsRequest);

            return Task.CompletedTask;
        }
    }
}