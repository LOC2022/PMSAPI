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
    public class MembershipProvider
    {
        public class MembershipsProvider : IMembershipProvider
        {
            private readonly IMembershipRepository _membershipRepository;
            private readonly ILogger _logger;

            public MembershipsProvider(IMembershipRepository membershipRepository, ILogger logger)
            {
                _membershipRepository = membershipRepository;
                _logger = logger;
            }

            public async Task AddOrModifyFeatureMaster(FeatureMaster featureMasterRequest)
            {

                try
                {
                    _logger.ForContext("featureMasterRequest", featureMasterRequest)
                        .Information("Add or update Feature request - Start");

                    //business logic

                    await _membershipRepository.ModifyFeatureMaster(featureMasterRequest);

                    _logger.ForContext("featureMasterRequest", featureMasterRequest)
                        .Information("Add or update Feature request - End");
                }
                catch (Exception exception)
                {
                    _logger.ForContext("featureMasterRequest", featureMasterRequest)
                        .Error(exception, "Exception occurred during Add or update Feature.");
                    await Task.FromException(exception);
                }
            }

            public async Task AddOrModifyGroupMaster(GroupMaster groupMasterRequest)
            {
                try
                {
                    _logger.ForContext("groupMasterRequest", groupMasterRequest)
                        .Information("Add or update group request - Start");

                    //business logic

                    await _membershipRepository.ModifyGroupMaster(groupMasterRequest);

                    _logger.ForContext("groupMasterRequest", groupMasterRequest)
                        .Information("Add or update group request - End");
                }
                catch (Exception exception)
                {
                    _logger.ForContext("groupMasterRequest", groupMasterRequest)
                        .Error(exception, "Exception occurred during Add group.");
                    await Task.FromException(exception);
                }
            }

            public async Task AddOrModifyUserMaster(UserMaster userMasterRequest)
            {
                try
                {
                    _logger.ForContext("userMasterRequest", userMasterRequest)
                        .Information("Add or update User request - Start");

                    //business logic

                    await _membershipRepository.ModifyUserMaster(userMasterRequest);

                    _logger.ForContext("userMasterRequest", userMasterRequest)
                        .Information("Add or update User request - End");
                }
                catch (Exception exception)
                {
                    _logger.ForContext("userMasterRequest", userMasterRequest)
                        .Error(exception, "Exception occurred during Add User.");
                    await Task.FromException(exception);
                }
            }

            public async Task<IEnumerable<FeatureMaster>> GetFeatureById(int groupId)
            {
                try
                {
                    _logger.ForContext("groupId", groupId)
                        .Information($"Get feature for the specific feature Id. - {groupId}.");

                    //business logic

                    return await _membershipRepository.SelectFeatureById(groupId);
                }
                catch (Exception exception)
                {
                    _logger.ForContext("groupId", groupId)
                        .Error(exception,
                            $"Exception occurred while trying to get feature for specific feature Id - {groupId}.");
                    await Task.FromException(exception);
                }

                return null;
            }

            public async Task<IEnumerable<GroupMaster>> GetGroupById(int groupId)
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

            public async Task<IEnumerable<UserMaster>> GetUserById(int userId)
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
        }
    }
}