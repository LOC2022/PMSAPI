using System.ComponentModel.DataAnnotations;

namespace LOC.PMS.Model
{
    public class MembershipMasters
    {
        public UserMaster UserMasterDetails { get; set; }

        public GroupMaster GroupMasterDetails { get; set; }

        public FeatureMaster FeatureMasterDetails { get; set; }

    }

    public class FeatureMaster
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
    }

    public class GroupMaster
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }

    public class UserMaster
    {
        public string Name { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Group { get; set; }

        public bool IsActive { get; set; }

    }
}