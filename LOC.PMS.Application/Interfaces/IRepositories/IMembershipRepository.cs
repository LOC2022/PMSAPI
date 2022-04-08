using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IMembershipRepository
    {
        Task<int> ModifyFeatureMaster(FeatureMaster featureMasterRequest);

        Task<int> AddGroup(AddGroupRequest addGroupRequest);

        Task<int> ModifyUserMaster(UserMaster userMasterRequest);

        Task<IEnumerable<FeatureMaster>> SelectFeatureById(int featureId);

        Task<IEnumerable<GroupDetails>> SelectGroupById(int groupId);

        Task<IEnumerable<UserMaster>> SelectUserById(int userId);

        Task DeactivateUser(int userId);

        Task DeactivateFeature(int featureId);

        Task DeactivateGroup(int groupId);

        Task<int> ActivateGroup(string groupName);

        Task<IEnumerable<MembershipMasters>> MemebershipLogin(string userName, string password);
    }
}