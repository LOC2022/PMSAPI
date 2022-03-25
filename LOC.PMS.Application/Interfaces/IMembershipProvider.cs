using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces
{
    public interface IMembershipProvider
    {
        Task AddOrModifyUserMaster(UserMaster userMasterRequest);

        Task AddOrModifyGroupMaster(GroupMaster groupMasterRequest);

        Task AddOrModifyFeatureMaster(FeatureMaster featureMasterRequest);

        Task<IEnumerable<FeatureMaster>> GetFeatureById(int featureId);

        Task<IEnumerable<GroupMaster>> GetGroupById(int groupId);

        Task<IEnumerable<UserMaster>> GetUserById(int userId);
    }
}