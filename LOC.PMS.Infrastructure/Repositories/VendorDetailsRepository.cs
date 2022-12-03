using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class VendorDetailsRepository : IVendorDetailsRepository
    {
        private readonly IContext _context;

        public VendorDetailsRepository(IContext context)
        {
            _context = context;
        }

        public async Task<int> ModifyVendorDetails(VendorMaster vendorMasterRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@VendorId", vendorMasterRequest.VendorId),
                new SqlParameter("@VendorName", vendorMasterRequest.VendorName),
                new SqlParameter("@Email", vendorMasterRequest.Email),
                new SqlParameter("@Phone", vendorMasterRequest.Phone),
                new SqlParameter("@City", vendorMasterRequest.City),
                new SqlParameter("@State", vendorMasterRequest.State),
                new SqlParameter("@BillToAddress", vendorMasterRequest.BillToAddress),
                new SqlParameter("@ShipToAddress", vendorMasterRequest.ShipToAddress),
                new SqlParameter("@Pincode", vendorMasterRequest.Pincode),
                new SqlParameter("@GSTNo", vendorMasterRequest.GSTNo),
                new SqlParameter("@NonD2LDays", vendorMasterRequest.NonD2LDays),
                new SqlParameter("@IsActive", vendorMasterRequest.IsActive),
                new SqlParameter("@ModifiedBy", vendorMasterRequest.ModifiedBy),
                new SqlParameter("@ciplVendorCode", string.IsNullOrEmpty(vendorMasterRequest.ciplVendorCode)?"":vendorMasterRequest.ciplVendorCode),
                new SqlParameter("ReturnVendorId",SqlDbType.Int){Direction = ParameterDirection.ReturnValue}
            };

            return await _context.ExecuteStoredProcedureAsync<int>("[dbo].[VendorMaster_Modify]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<VendorMaster>> SelectVendorDetails(int vendorId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@vendorId", vendorId)
            };

            return await _context.QueryStoredProcedureAsync<VendorMaster>("[dbo].[VendorMaster_Select]", sqlParams.ToArray());
        }

        public Task DeactivateVendorById(int vendorId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@VendorId", vendorId)
            };
            _context.ExecuteStoredProcedure("[dbo].[VendorMaster_Deactivate]", sqlParams.ToArray());
            return Task.CompletedTask;
        }
    }
}