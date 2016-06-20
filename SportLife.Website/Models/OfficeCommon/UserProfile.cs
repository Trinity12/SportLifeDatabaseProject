using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportLife.Website.Models.OfficeCommon {
    public class UserProfile {
        [ReadOnly(true)]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [DataType(DataType.Url)]
        public UrlAttribute Avatar { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}