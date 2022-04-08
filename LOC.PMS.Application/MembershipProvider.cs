using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Serilog;

namespace LOC.PMS.Application
{
    public class MembershipsProvider:IMembershipProvider
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly ILogger _logger;
        private const int DefaultReturnValue = 0;

        public MembershipsProvider(IMembershipRepository membershipRepository, ILogger logger)
        {
            _membershipRepository = membershipRepository;
            _logger = logger;
        }

        public async Task<int> AddOrModifyFeatureMaster(FeatureMaster featureMasterRequest)
        {
            var returnFeatureId = DefaultReturnValue;

            try
            {
                _logger.ForContext("featureMasterRequest", featureMasterRequest)
                    .Information("Add or update Feature request - Start");

                //business logic

                returnFeatureId = await _membershipRepository.ModifyFeatureMaster(featureMasterRequest);

                _logger.ForContext("featureMasterRequest", featureMasterRequest)
                    .Information("Add or update Feature request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("featureMasterRequest", featureMasterRequest)
                    .Error(exception, "Exception occurred during Add or update Feature.");
                await Task.FromException(exception);
            }

            return returnFeatureId;
        }

        public async Task<int> AddGroup(AddGroupRequest addGroupRequest)
        {
            var returnGroupId = DefaultReturnValue;

            try
            {
                _logger.ForContext("addGroupRequest", addGroupRequest)
                    .Information("Add or update group request - Start");

                //business logic

                returnGroupId = await _membershipRepository.AddGroup(addGroupRequest);

                _logger.ForContext("addGroupRequest", addGroupRequest)
                    .Information("Add or update group request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("addGroupRequest", addGroupRequest)
                    .Error(exception, "Exception occurred during Add group.");
                await Task.FromException(exception);
            }

            return returnGroupId;
        }

        public async Task<int> AddOrModifyUserMaster(UserMaster userMasterRequest)
        {
            var returnUserId = DefaultReturnValue;

            try
            {
                _logger.ForContext("userMasterRequest", userMasterRequest)
                    .Information("Add or update User request - Start");

                //business logic

                returnUserId = await _membershipRepository.ModifyUserMaster(userMasterRequest);

                _logger.ForContext("userMasterRequest", userMasterRequest)
                    .Information("Add or update User request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("userMasterRequest", userMasterRequest)
                    .Error(exception, "Exception occurred during Add User.");
                await Task.FromException(exception);
            }

            return returnUserId;
        }

        public async Task<IEnumerable<FeatureMaster>> GetFeatures(int featureId)
        {
            try
            {
                _logger.ForContext("featureId", featureId)
                    .Information($"Get feature for the specific feature Id. - {featureId}.");

                //business logic

                return await _membershipRepository.SelectFeatureById(featureId);
            }
            catch (Exception exception)
            {
                _logger.ForContext("featureId", featureId)
                    .Error(exception,
                        $"Exception occurred while trying to get feature for specific feature Id - {featureId}.");
                await Task.FromException(exception);
            }

            return null;
        }

        public async Task<IEnumerable<GroupDetails>> GetGroups(int groupId)
        {
            try
            {
                _logger.ForContext("groupId", groupId)
                    .Information($"Get group for the specific group Id. - {groupId}.");

                //business logic

                return await _membershipRepository.SelectGroupById(groupId);
            }
            catch (Exception exception)
            {
                _logger.ForContext("groupId", groupId)
                    .Error(exception,
                        $"Exception occurred while trying to get group for specific group Id - {groupId}.");
                await Task.FromException(exception);
            }

            return null;
        }

        public async Task<IEnumerable<UserMaster>> GetUsers(int userId)
        {
            try
            {
                _logger.ForContext("userId", userId)
                    .Information($"Get user for the specific user Id. - {userId}.");

                //business logic

                return await _membershipRepository.SelectUserById(userId);
            }
            catch (Exception exception)
            {
                _logger.ForContext("userId", userId)
                    .Error(exception, $"Exception occurred while trying to get user for specific user Id - {userId}.");
                await Task.FromException(exception);
            }

            return null;
        }

        public async Task DeactivateUserById(int userId)
        {
            try
            {
                _logger.ForContext("userId", userId)
                    .Information("Deactivate User request - Start");

                //business logic

                await _membershipRepository.DeactivateUser(userId);

                _logger.ForContext("userId", userId)
                    .Information("Deactivate User request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("userId", userId)
                    .Error(exception, $"Exception occurred during User Deactivate operation for user id. - {userId}");
                await Task.FromException(exception);
            }
        }

        public async Task DeactivateFeatureById(int featureId)
        {
            try
            {
                _logger.ForContext("featureId", featureId)
                    .Information("Deactivate Feature request - Start");

                //business logic

                await _membershipRepository.DeactivateFeature(featureId);

                _logger.ForContext("featureId", featureId)
                    .Information("Deactivate Feature request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("featureId", featureId)
                    .Error(exception, $"Exception occurred during Feature Deactivate operation for feature id. - {featureId}");
                await Task.FromException(exception);
            }
        }

        public async Task DeactivateGroupById(int groupId)
        {
            try
            {
                _logger.ForContext("groupId", groupId)
                    .Information("Deactivate Group request - Start");

                //business logic

                await _membershipRepository.DeactivateGroup(groupId);

                _logger.ForContext("groupId", groupId)
                    .Information("Deactivate Group request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("featureId", groupId)
                    .Error(exception, $"Exception occurred during Group Deactivate operation for groupId id. - {groupId}");
                await Task.FromException(exception);
            }
        }

        public async Task<int> ActivateGroupByName(string groupName)
        {
            var activatedGroupId = DefaultReturnValue;

            try
            {
                _logger.ForContext("groupName", groupName)
                    .Information("Activate/ReActivate Group request - Start");

                //business logic

                activatedGroupId = await _membershipRepository.ActivateGroup(groupName);

                _logger.ForContext("groupName", groupName)
                    .Information("Activate/ReActivate Group request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("groupName", groupName)
                    .Error(exception, $"Exception occurred during Activate/ReActivate Group operation for group Name. - {groupName}");
                await Task.FromException(exception);
            }

            return activatedGroupId;
        }

         public async Task<IEnumerable<MembershipMasters>> MembershipLogin(string userName, string password)
        {
            try
            {
                _logger.ForContext("LoginUser", userName)
                    .Information($"Login Request for User Name - {userName} - Start");


                return await _membershipRepository.MemebershipLogin(userName, password);

                //_logger.ForContext("LoginUser", userName)
                    //.Information($"Login Request for User Name - {userName} - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("featureId", userName)
                    .Error(exception,
                        $"Exception occurred while trying to login Request for User - {userName}.");
                await Task.FromException(exception);
            }

            return null;

        }
    }
}