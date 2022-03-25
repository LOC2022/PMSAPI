using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// Pallet Details Controller.
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipProvider _membershipProvider;

        /// <summary>
        /// Pallet Details Controller constructor.
        /// </summary>
        /// <param name="membershipProvider"></param>
        public MembershipController(IMembershipProvider membershipProvider)
        {
            this._membershipProvider = membershipProvider;
        }

        /// <summary>
        /// Add new pallet to the server.
        /// </summary>
        /// <param name="palletDetailsRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add or Modify User Master.",
            Tags = new[] { "AddOrModifyUserMaster" },
            OperationId = "AddOrModifyUserMaster")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddOrModifyUserMaster"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyUserMaster([FromBody] UserMaster palletDetailsRequest)
        {
            await _membershipProvider.AddOrModifyUserMaster(palletDetailsRequest);
            return Ok();
        }

        /// <summary>
        /// Add or Modify Group Master..
        /// </summary>
        /// <param name="groupMasterRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add or Modify Group Master.",
            Tags = new[] { "AddOrModifyGroupMaster" },
            OperationId = "AddOrModifyGroupMaster")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddOrModifyGroupMaster"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyGroupMaster([FromBody] GroupMaster groupMasterRequest)
        {
            await _membershipProvider.AddOrModifyGroupMaster(groupMasterRequest);
            return Ok();
        }

        /// <summary>
        /// Add or Modify Feature Master.
        /// </summary>
        /// <param name="featureMasterRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add or Modify Feature Master.",
            Tags = new[] { "AddOrModifyFeatureMaster" },
			OperationId = "AddOrModifyGroupMaster")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddOrModifyFeatureMaster"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyFeatureMaster([FromBody] FeatureMaster featureMasterRequest)
        {
            await _membershipProvider.AddOrModifyFeatureMaster(featureMasterRequest);
            return Ok();
        }

        /// <summary>
        /// Get Feature for the specified FeatureID from Feature Master
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get the the specified Feature from Feature Master.",
            Tags = new[] { "GetFeatureById" },
            OperationId = "GetFeatureById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetFeatureById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetFeatureById([FromQuery] int featureId)
        {
            var response = await _membershipProvider.GetFeatureById(featureId);
            return Ok(response);
        }

        /// <summary>
        /// Get Group for the specified GroupID from Group Master.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get the the specified Group from Group Master.",
            Tags = new[] { "GetGroupById" },
            OperationId = "GetGroupById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetGroupById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetGroupById([FromQuery] int groupId)
        {
            var response = await _membershipProvider.GetGroupById(groupId);
            return Ok(response);
        }

        /// <summary>
        /// Get Feature for the specified FeatureID from Feature Master
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get the the specified Feature from Feature Master.",
            Tags = new[] { "GetUserById" },
            OperationId = "GetUserById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetUserById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUserById([FromQuery] int userId)
        {
            var response= await _membershipProvider.GetUserById(userId);
            return Ok(response);
        }
    }
}