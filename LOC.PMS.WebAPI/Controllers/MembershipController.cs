using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace LOC.PMS.WebAPI.Controllers
{
    /// <summary>
    /// Memebership Controller.
    /// </summary>
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/PMSAPI/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipProvider _membershipProvider;

        private const int GETALL = 0;

        /// <summary>
        /// Memebership Controller constructor.
        /// </summary>
        /// <param name="membershipProvider"></param>
        public MembershipController(IMembershipProvider membershipProvider)
        {
            this._membershipProvider = membershipProvider;
        }

        /// <summary>
        /// Add or Modify User Master.
        /// </summary>
        /// <param name="userDetailsRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add or Modify User Master.",
            Tags = new[] { "AddOrModifyUserMaster" },
            OperationId = "AddOrModifyUserMaster")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddOrModifyUserMaster"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddOrModifyUserMaster([FromBody] UserMaster userDetailsRequest)
        {
           var response = await _membershipProvider.AddOrModifyUserMaster(userDetailsRequest);
            return Ok(response);
        }

        /// <summary>
        /// Add or Modify Group Master..
        /// </summary>
        /// <param name="addGroupRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Add or Modify Group Master.",
            Tags = new[] { "AddOrModifyGroupMaster" },
            OperationId = "AddOrModifyGroupMaster")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPost("AddGroup"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupRequest addGroupRequest)
        {
            var response = await _membershipProvider.AddGroup(addGroupRequest);
            return Ok(response);
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
            var response = await _membershipProvider.AddOrModifyFeatureMaster(featureMasterRequest);
            return Ok(response);
        }

        /// <summary>
        /// Get the User for the specified feature Id from Feature Master. If feature id is not specified then it will return all records.
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get the Features for the specified feature Id from Feature Master. If feature id is not specified then it will return all records.",
            Tags = new[] { "GetFeatures" },
            OperationId = "GetFeatures")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetFeatures"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetFeatures([FromQuery] int featureId = GETALL)
        {
            var response = await _membershipProvider.GetFeatures(featureId);
            return Ok(response);
        }

        /// <summary>
        /// Get the Group and its enabled rights for the active features, based on specified Group id. If Group id is not specified then it will return all records.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get the Group and its enabled rights for the active features, based on specified Group id. If Group id is not specified then it will return all records.",
                          Tags = new[] { "GetGroups" },
            OperationId = "GetGroups")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetGroups"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetGroups([FromQuery] int groupId = GETALL)
        {
            var response = await _membershipProvider.GetGroups(groupId);
            return Ok(response);
        }

        /// <summary>
        /// Get the User for the specified User Id from User Master. If the User id is not specified then it will return all records.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Get the User for the specified User Id from User Master. If the User id is not specified then it will return all records.",
            Tags = new[] { "GetUserById" },
            OperationId = "GetUserById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("GetUsers"), MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUsers([FromQuery] int userId = GETALL)
        {
            var response= await _membershipProvider.GetUsers(userId);
            return Ok(response);
        }

        /// <summary>
        ///  Deactivate feature details.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Deactivate the feature details for the specified feature id.",
            Tags = new[] { "DeactivateFeatureById" },
            OperationId = "DeactivateFeatureById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPut("DeactivateFeatureById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> DeactivateFeatureById(int featureId)
        {
            await _membershipProvider.DeactivateFeatureById(featureId);
            return Ok();
        }

        /// <summary>
        ///  Deactivate Group details.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Deactivate the Group details for the specified Group id.",
            Tags = new[] { "DeactivateGroupById" },
            OperationId = "DeactivateGroupById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPut("DeactivateGroupById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> DeactivateGroupById(int groupId)
        {
            await _membershipProvider.DeactivateGroupById(groupId);
            return Ok();
        }

        /// <summary>
        ///  Deactivate User details.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Deactivate the User details for the specified User id.",
            Tags = new[] { "DeactivateUserById" },
            OperationId = "DeactivateUserById")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPut("DeactivateUserById"), MapToApiVersion("1.0")]
        public async Task<IActionResult> DeactivateUserById(int userId)
        {
            await _membershipProvider.DeactivateUserById(userId);
            return Ok();
        }

        /// <summary>
        ///  Activate/Re-Activate the Group.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Activate/Re-Activate the Group",
            Tags = new[] { "ActivateGroupByName" },
            OperationId = "ActivateGroupByName")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpPut("ActivateGroupByName"), MapToApiVersion("1.0")]
        public async Task<IActionResult> ActivateGroupByName(string groupName)
        {
            await _membershipProvider.ActivateGroupByName(groupName);
            return Ok();
        }

             /// <summary>
        ///  Login.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(
            Description = "Login",
            Tags = new[] { "MembershipLogin" },
            OperationId = "MembershipLogin")]
        [SwaggerResponse(200, "OK", typeof(StatusCodeResult))]
        [SwaggerResponse(400, "Bad Request", typeof(StatusCodeResult))]
        [SwaggerResponse(500, "Internal Server Error.", typeof(StatusCodeResult))]
        [HttpGet("MembershipLogin"), MapToApiVersion("1.0")]
        public async Task<IActionResult> MembershipLogin(string UserName, string password)
        {
           var response = await _membershipProvider.MembershipLogin(UserName, password);
            return Ok(response);
        }
    }
}