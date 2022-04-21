using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportDetailsProvider _reportDetailsProvider;
        private const string GETALL = "";
        private const int DefaultValue = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportDetailsProvider"></param>
        public ReportsController(IReportDetailsProvider reportDetailsProvider)
        {
            this._reportDetailsProvider = reportDetailsProvider;
        }



        /// <summary>
        /// GetMonthlyPlanReport
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="VendorId"></param>        
        /// <returns>List of Orders</returns>
        [SwaggerOperation(
            Description = "GetMonthlyPlanReport",
            Tags = new[] { "GetMonthlyPlanReport" },
            OperationId = "GetMonthlyPlanReport")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetMonthlyPlanReport"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetMonthlyPlanReport([FromQuery] string FromDate= GETALL, [FromQuery] string ToDate= GETALL, [FromQuery] int VendorId= DefaultValue)
        {
            var response = await _reportDetailsProvider.GetMonthlyPlanReport(FromDate, ToDate, VendorId);
            return Ok(response);
        }

    }
}
