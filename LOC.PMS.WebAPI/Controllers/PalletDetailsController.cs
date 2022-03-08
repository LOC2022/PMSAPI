using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Microsoft.AspNetCore.Mvc;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class PalletDetailsController : ControllerBase
    {
        private readonly IPalletDetailsProvider _palletDetailsProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palletDetailsProvider"></param>
        public PalletDetailsController(IPalletDetailsProvider palletDetailsProvider)
        {
            this._palletDetailsProvider = palletDetailsProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palletDetailsRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPallet(PalletDetailsRequest palletDetailsRequest)
        {
            await _palletDetailsProvider.AddPallet(palletDetailsRequest);
            return Ok();
        }
    }
}