using System.ComponentModel.DataAnnotations;

namespace SportLife.Website.Models.OfficeCommon {
    public class RoleViewModel {
        [DataType(DataType.Text)]
        public string RoleName { get; set; }
    }
}