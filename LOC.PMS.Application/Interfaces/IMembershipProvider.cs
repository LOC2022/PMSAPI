using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IMembershipProvider
    {
        Task<int> AddOrModifyUserMaster(UserMaster userMasterRequest);

        Task<int> AddGroup(AddGroupRequest addGroupRequest);

        Task<int> AddOrModifyFeatureMaster(FeatureMaster featureMasterRequest);

        Task<IEnumerable<FeatureMaster>> GetFeatures(int featureId);

        Task<IEnumerable<GroupDetails>> GetGroups(int groupId);

        Task<IEnumerable<UserMaster>> GetUsers(int userId);

        Task DeactivateUserById(int userId);

        Task DeactivateFeatureById(int featureId);

        Task DeactivateGroupById(int groupId);

        Task<int> ActivateGroupByName(string groupName);

        Task<IEnumerable<MembershipMasters>> MembershipLogin(string userName, string password);
    }
}