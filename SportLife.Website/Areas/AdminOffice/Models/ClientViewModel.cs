using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportLife.Website.Areas.AdminOffice.Models {
    public class ClientViewModel {
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
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }

    public class ClientFullViewModel {
        public int ID { get; set; }

        public int Avatar { get; set; }

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
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [ReadOnly(true)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }
}