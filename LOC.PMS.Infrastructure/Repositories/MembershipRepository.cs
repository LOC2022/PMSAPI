using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly IContext _context;

        public MembershipRepository(IContext context)
        {
            _context = context;
        }

        public Task ModifyFeatureMaster(FeatureMaster featureMasterRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FeatureName", featureMasterRequest.FeatureName),
            };

            _context.ExecuteStoredProcedure("[dbo].[FeatureMaster_Modify]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task ModifyGroupMaster(GroupMaster groupMasterRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@GroupName", groupMasterRequest.GroupName)
            };

            _context.ExecuteStoredProcedure("[dbo].[GroupMaster_Modify]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task ModifyUserMaster(UserMaster userMasterRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserName", userMasterRequest.Name),
                new SqlParameter("@UserEmail", userMasterRequest.Email),
                new SqlParameter("@IsActive", userMasterRequest.IsActive),
                new SqlParameter("@Password", userMasterRequest.Password),
                new SqlParameter("@PhoneNumber", userMasterRequest.PhoneNumber),
                new SqlParameter("@Group", userMasterRequest.Group)
            };

            _context.ExecuteStoredProcedure("[dbo].[UserMaster_Modify]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task<IEnumerable<FeatureMaster>> SelectFeatureById(int featureId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FeatureId", featureId)
            };

            return _context.QueryStoredProcedureAsync<FeatureMaster>("[dbo].[FeatureMaster_SelectById]", sqlParams.ToArray());
        }

        public Task<IEnumerable<GroupMaster>> SelectGroupById(int groupId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@GroupId", groupId)
            };

            return _context.QueryStoredProcedureAsync<GroupMaster>("[dbo].[GroupMaster_SelectById]", sqlParams.ToArray());
        }

        public Task<IEnumerable<UserMaster>> SelectUserById(int userId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", userId)
            };

            return _context.QueryStoredProcedureAsync<UserMaster>("[dbo].[UserMaster_SelectById]", sqlParams.ToArray());
        }
    }
}