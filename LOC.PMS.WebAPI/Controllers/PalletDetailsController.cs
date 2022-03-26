using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// Pallet Details Controller.
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class PalletDetailsController : ControllerBase
    {
        private readonly IPalletDetailsProvider _palletDetailsProvider;

        private const int GETALL = 1;

        /// <summary>
        /// Pallet Details Controller constructor.
        /// </summary>
        /// <param name="palletDetailsProvider"></param>
        public PalletDetailsController(IPalletDetailsProvider palletDetailsProvider)
        {
            this._palletDetailsProvider = palletDetailsProvider;
        }

        /// <summary>
        /// Add new pallet to the server.
        /// </summary>
        /// <param name="palletDetailsRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add new pallet to the server.",
            Tags = new[] { "AddOrModifyPallet" },
            OperationId = "AddOrModifyPallet")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddOrModifyPallet"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyPallet([FromBody] PalletDetails palletDetailsRequest)
        {
            await _palletDetailsProvider.ModifyPalletDetails(palletDetailsRequest);
            return Ok();
        }

        /// <summary>
        /// Add new pallet to the server.
        /// </summary>
        /// <param name="palletLocationDetailsRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add or modify new pallet Location",
            Tags = new[] { "AddOrModifyPalletLocation" },
            OperationId = "AddOrModifyPalletLocation")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddOrModifyPalletLocation"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyPalletLocation([FromBody] LocationMaster palletLocationDetailsRequest)
        {
            await _palletDetailsProvider.ModifyPalletLocation(palletLocationDetailsRequest);
            return Ok();
        }

        /// <summary>
        /// Get Pallet details for the specified pallet id. If pallet id is not specified then it will return all pallets from pallet master.
        /// </summary>
        /// <param name="palletId"></param>
        /// <returns>List of PalletDetails</returns>
        [SwaggerOperation(
            Description = "Get Pallet details.",
            Tags = new[] { "GetPallets" },
            OperationId = "GetPallets")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPallets"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPallets([FromQuery] int palletId = GETALL)
        {
            var response = await _palletDetailsProvider.GetPalletDetails(palletId);
            return Ok(response);
        }

        /// <summary>
        /// Get Pallet location details for the specified location id. If location id is not specified then it will return all locations from location master.
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>List of PalletDetails</returns>
        [SwaggerOperation(
            Description = "Get Pallet location details.",
            Tags = new[] { "GetPalletLocationDetails" },
            OperationId = "GetPalletLocationDetails")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetPalletLocationDetails"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPalletLocationDetails([FromQuery] int locationId = GETALL)
        {
            var response = await _palletDetailsProvider.GetPalletLocation(locationId);
            return Ok(response);
        }

        /// <summary>
        /// Delete Pallet details.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Delete the Pallet details for the specified pallet id.",
            Tags = new[] { "DeletePalletByPalletId" },
            OperationId = "DeletePalletByPalletId")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpDelete("DeletePalletByPalletId"), MapToApiVersion("1.0")]
        public async Task<IActionResult> DeletePalletByPalletId([FromQuery] int palletId)
        {
            await _palletDetailsProvider.DeletePalletByPalletId(palletId);
            return Ok();
        }

        /// <summary>
        /// Get all Pallet details.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Delete the Pallet location details for the specified location id.",
            Tags = new[] { "DeletePalletLocationById" },
            OperationId = "DeletePalletLocationById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("DeletePalletLocationById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> DeletePalletLocationById(int locationId)
        {
            await _palletDetailsProvider.DeletePalletLocationById(locationId);
            return Ok();
        }
    }
}