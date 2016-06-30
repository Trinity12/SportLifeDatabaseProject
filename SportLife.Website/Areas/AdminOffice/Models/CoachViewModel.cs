using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportLife.Website.Areas.AdminOffice.Models {
    public class CoachViewModel {
        public int ID { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }

    public class CoachFullViewModel {
        public int ID { get; set; }

        public int? Avatar { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }

    public class CoachEditViewModel
    {
        public int ID { get; set; }
    }

    public class CoachDropDownViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
    }

    public class CoachGroupDetailViewModel
    {
        public int ID { get; set; }

        public int? Avatar { get; set; }

        public string FullName { get; set; }
    }
}