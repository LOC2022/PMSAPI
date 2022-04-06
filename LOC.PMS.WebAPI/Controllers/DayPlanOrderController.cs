using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// DayPlanOrderController
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class DayPlanOrderController : ControllerBase
    {
        private readonly IOrdesDetailProvider _ordesDetailProvider;


        /// <summary>
        /// Pallet Details Controller constructor.
        /// </summary>
        /// <param name="ordesDetailProvider"></param>
        public DayPlanOrderController(IOrdesDetailProvider ordesDetailProvider)
        {
            this._ordesDetailProvider = ordesDetailProvider;
        }

        
        

        private async void CreateOrder()
        {
            await _ordesDetailProvider.CreateOrder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "A.",
            Tags = new[] { "PostSuccess" },
            OperationId = "PostSuccess")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("ImportDayPlanTest"), MapToApiVersion("1.0")]
        public async Task<IActionResult> ImportDayPlan([FromForm] FileModel files)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", files.FileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                files.FormFile.CopyTo(stream);
            }
            List<DayPlan> Order = new List<DayPlan>();

            Order = System.IO.File.ReadAllLines(path)
                .Skip(1)
                .Select(v => DayPlan.FromCsv(v))
                .ToList();
            await _ordesDetailProvider.AddDayPlanData(Order);            
            return Ok();
        }

    }
}
