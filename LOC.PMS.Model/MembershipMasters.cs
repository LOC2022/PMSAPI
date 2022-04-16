using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LOC.PMS.Model
{
    public class MembershipMasters
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string ActiveFeatures { get; set; }

    }

    public class FeatureMaster
    {
        public int FeatureId { get; set; }

        public string FeatureName { get; set; }
    }

    public class AddGroupRequest
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public List<GroupFeatures> GroupFeaturesList { get; set; }

        public string ModifiedBy { get; set; }
    }

    public class UserMaster
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public bool IsActive { get; set; }

        public string ModifiedBy { get; set; }

    }

    public class GroupFeatures
    {
        public int FeatureId { get; set; }

        public bool IsEnabled { get; set; }

    }

    public class GroupRights
    {
        public int GroupRightsId { get; set; }

        public int GroupId { get; set; }

        public int FeatureId { get; set; }

        public bool IsEnabled { get; set; }

        public string ModifiedBy { get; set; }

    }

    public class GroupDetails
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string ActiveFeatures { get; set; }

    }
}