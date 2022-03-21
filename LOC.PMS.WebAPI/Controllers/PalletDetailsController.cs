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
            Tags = new[] { "PostSuccess" },
            OperationId = "PostSuccess")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddPallet"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyPallet([FromBody] PalletDetails palletDetailsRequest)
        {
            await _palletDetailsProvider.AddOrModifyPallet(palletDetailsRequest);
            return Ok();
        }

        /// <summary>
        /// Get Pallet details for the specified pallet part no.
        /// </summary>
        /// <param name="palletPartNo"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get Pallet details for the specified pallet part no.",
            Tags = new[] { "PostSuccess" },
            OperationId = "PostSuccess")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("GetPalletByPartNo"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPalletByPartNo([FromQuery] string palletPartNo)
        {
            IEnumerable<PalletDetails> response = await _palletDetailsProvider.GetPalletDetails(palletPartNo);
            return Ok(response);
        }

        /// <summary>
        /// Get all Pallet details.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get all Pallet details.",
            Tags = new[] { "PostSuccess" },
            OperationId = "PostSuccess")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("GetAllPallet"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllPallet()
        {
            IEnumerable<PalletDetails> response = await _palletDetailsProvider.GetPalletDetails("ALL");
            return Ok(response);
        }

        /// <summary>
        /// Get all Pallet details.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Delete the Pallet details for the specified pallet part no.",
            Tags = new[] { "PostSuccess" },
            OperationId = "PostSuccess")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("DeletePalletByPartNo"), MapToApiVersion("1.0")]
        public async Task<IActionResult> DeletePalletByPartNo(int palletId)
        {
            await _palletDetailsProvider.DeletePalletByPartNo(palletId);
            return Ok();
        }

    }
}