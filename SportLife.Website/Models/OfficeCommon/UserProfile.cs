using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportLife.Website.Models.OfficeCommon {
    public class UserProfile {
        [ReadOnly(true)]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        public Uri Avatar { get; set; }

        public RoleViewModel Role { get; set; }
    }
}