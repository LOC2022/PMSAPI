using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IMembershipRepository
    {
        Task ModifyFeatureMaster(FeatureMaster featureMasterRequest);

        Task ModifyGroupMaster(GroupMaster groupMasterRequest);

        Task ModifyUserMaster(UserMaster userMasterRequest);

        Task<IEnumerable<FeatureMaster>> SelectFeatureById(int featureId);

        Task<IEnumerable<GroupMaster>> SelectGroupById(int groupId);

        Task<IEnumerable<UserMaster>> SelectUserById(int userId);

    }
}