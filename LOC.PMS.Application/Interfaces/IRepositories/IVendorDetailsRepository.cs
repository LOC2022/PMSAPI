using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IVendorDetailsRepository
    {
        Task<int> ModifyVendorDetails(VendorMaster vendorMasterRequest);

        Task<IEnumerable<VendorMaster>> SelectVendorDetails(int vendorId);

        Task DeactivateVendorById(int vendorId);

    }
}