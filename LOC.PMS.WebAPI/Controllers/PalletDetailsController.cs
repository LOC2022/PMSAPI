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
        public async Task<IActionResult> AddPallet([FromBody] PalletDetailsRequest palletDetailsRequest)
        {
            await _palletDetailsProvider.AddPallet(palletDetailsRequest);
            return Ok();
        }
    }
}