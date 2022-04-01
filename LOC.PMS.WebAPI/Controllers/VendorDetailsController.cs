using LOC.PMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOC.PMS.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace LOC.PMS.WebAPI.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class VendorDetailsController : ControllerBase
    {
        private readonly IVendorDetailsProvider _vendorDetailsProvider;

        private const int GETALL = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorDetailsProvider"></param>
        public VendorDetailsController(IVendorDetailsProvider vendorDetailsProvider)
        {
            _vendorDetailsProvider = vendorDetailsProvider;
        }

        /// <summary>
        /// Add or Modify vendor information.
        /// </summary>
        /// <param name="vendorDetailsRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add or Modify vendor information.",
            Tags = new[] { "AddOrModifyVendor" },
            OperationId = "AddOrModifyVendor")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddOrModifyVendor"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyVendor([FromBody] VendorMaster vendorDetailsRequest)
        {
            var response = await _vendorDetailsProvider.ModifyVendorDetails(vendorDetailsRequest);
            return Ok(response);
        }

        /// <summary>
        /// Get Vendor details for the specified Vendor id. If Vendor id is not specified then it will return all vendor from vendor master.
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns>List of PalletDetails</returns>
        [SwaggerOperation(
            Description = "Get Vendor details for the specified Vendor id. If Vendor id is not specified then it will return all locations from location master.",
            Tags = new[] { "GetVendorDetails" },
            OperationId = "GetVendorDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetVendorDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetVendorDetails([FromQuery] int vendorId = GETALL)
        {
            var response = await _vendorDetailsProvider.GetVendorDetails(vendorId);
            return Ok(response);
        }

        /// <summary>
        ///  Deactivate the vendor details for the specified vendor id.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Deactivate the vendor details for the specified vendor id.",
            Tags = new[] { "DeactivateVendorById" },
            OperationId = "DeactivateVendorById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("DeactivateVendorById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> DeactivateVendorById(int vendorId)
        {
            await _vendorDetailsProvider.DeactivateVendorById(vendorId);
            return Ok();
        }

    }
}
