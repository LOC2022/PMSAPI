using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dayPlan"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "A.",
            Tags = new[] { "PostSuccess" },
            OperationId = "PostSuccess")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        //[SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("ImportDayPlan"), MapToApiVersion("1.0")]
        public async Task<IActionResult> ImportDayPlan([FromBody] DayPlan _dayPlan)
        {
            List<DayPlan> Order = new List<DayPlan>();  
            string FPath = "C:\\Users\\Muthazagan R\\Desktop\\PMS\\TestFilr";
            string FileString =  _dayPlan.ByteArray.Replace("base64",string.Empty);
            var Doc=Convert.FromBase64String(FileString);
            string FilePath = Path.Combine(FPath, DateTime.Now.ToString("ddMMyyyy") + ".csv");
            System.IO.File.WriteAllBytes(FilePath, Doc);

            Order = System.IO.File.ReadAllLines(FilePath)
                .Skip(1)
                .Select(v => DayPlan.FromCsv(v))
                .ToList();
            await _palletDetailsProvider.AddDayPlanData(Order);
            return Ok();
        }



    }
}