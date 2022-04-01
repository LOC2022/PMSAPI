using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IVendorDetailsProvider
    {
        Task<int> ModifyVendorDetails(VendorMaster vendorDetailsRequest);

        Task<IEnumerable<VendorMaster>> GetVendorDetails(int vendorId);

        Task DeactivateVendorById(int vendorId);
    }
}