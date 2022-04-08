using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Z.Dapper.Plus;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly IContext _context;

        public MembershipRepository(IContext context)
        {
            _context = context;
        }

        public async Task<int> ModifyFeatureMaster(FeatureMaster featureMasterRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FeatureId", featureMasterRequest.FeatureId),
                new SqlParameter("@FeatureName", featureMasterRequest.FeatureName),
                new SqlParameter("ReturnFeatureId",SqlDbType.Int){Direction = ParameterDirection.ReturnValue}

            };

            return await _context.ExecuteStoredProcedureAsync<int>("[dbo].[FeatureMaster_Modify]", sqlParams.ToArray());

        }

        public async Task<int> AddGroup(AddGroupRequest addGroupRequest)
        {
            var returnGroupId = 0;
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@GroupName", addGroupRequest.GroupName),
                new SqlParameter("@ModifiedBy", addGroupRequest.ModifiedBy),
                new SqlParameter("ReturnGroupId",SqlDbType.Int){Direction = ParameterDirection.ReturnValue}
            };

            returnGroupId = await _context.ExecuteStoredProcedureAsync<int>("[dbo].[GroupMaster_Add]", sqlParams.ToArray());

            var groupRightsList = new List<GroupRights>();

            foreach (var feature in addGroupRequest.GroupFeaturesList)
            {
                groupRightsList.Add(new GroupRights()
                {
                    GroupId = returnGroupId,
                    FeatureId = feature.FeatureId,
                    IsEnabled = feature.IsEnabled,
                    ModifiedBy = addGroupRequest.ModifiedBy
                });
            }

            DapperPlusManager.Entity<GroupRights>().Table("GroupRights").InsertIfNotExists().Identity(i => i.GroupRightsId);

            _context.BulkCopy(groupRightsList, 30, true);

            return returnGroupId;
        }

        public async Task<int> ModifyUserMaster(UserMaster userMasterRequest)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", userMasterRequest.UserId),
                new SqlParameter("@UserName", userMasterRequest.UserName),
                new SqlParameter("@Password", userMasterRequest.Password),
                new SqlParameter("@GroupId", userMasterRequest.GroupId),
                new SqlParameter("@Phone", userMasterRequest.Phone),
                new SqlParameter("@Email", userMasterRequest.Email),
                new SqlParameter("@IsActive", userMasterRequest.IsActive),
                new SqlParameter("@ModifiedBy", userMasterRequest.ModifiedBy),
                new SqlParameter("ReturnUserId",SqlDbType.Int){Direction = ParameterDirection.ReturnValue}
            };

            return await _context.ExecuteStoredProcedureAsync<int>("[dbo].[UserMaster_Modify]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<FeatureMaster>> SelectFeatureById(int featureId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FeatureId", featureId)
            };

            return await _context.QueryStoredProcedureAsync<FeatureMaster>("[dbo].[FeatureMaster_Select]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<GroupDetails>> SelectGroupById(int groupId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@GroupId", groupId)
            };

            return await _context.QueryStoredProcedureAsync<GroupDetails>("[dbo].[GroupDetails_Select]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<UserMaster>> SelectUserById(int userId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", userId)
            };

            return await _context.QueryStoredProcedureAsync<UserMaster>("[dbo].[UserMaster_Select]", sqlParams.ToArray());
        }

        public Task DeactivateUser(int userId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", userId)
            };
            _context.ExecuteStoredProcedure("[dbo].[UserMaster_Deactivate]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task DeactivateFeature(int featureId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FeatureId", featureId)
            };
            _context.ExecuteStoredProcedure("[dbo].[FeatureMaster_Deactivate]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public Task DeactivateGroup(int groupId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@GroupId", groupId)
            };
            _context.ExecuteStoredProcedure("[dbo].[Group_Deactivate]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public async Task<int> ActivateGroup(string groupName)
        {
            var ModifiedBy = "";
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@GroupName", groupName),
                new SqlParameter("@ModifiedBy", ModifiedBy),
                new SqlParameter("ReturnActivatedGroupId",SqlDbType.Int){Direction = ParameterDirection.ReturnValue}
            };

            return await _context.ExecuteStoredProcedureAsync<int>("[dbo].[Group_Activate]", sqlParams.ToArray());

        }

        public async Task<IEnumerable<MembershipMasters>> MemebershipLogin(string userName, string password)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password)

                };

           var resp = await _context.QueryStoredProcedureAsync<MembershipMasters>("[dbo].[MemebershipLogin_Select]", sqlParams.ToArray());

            return resp;
                
        }
    }
}