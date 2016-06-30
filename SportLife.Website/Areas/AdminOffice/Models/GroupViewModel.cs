using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportLife.Website.Areas.AdminOffice.Models {
    public class GroupViewModel {
        public int ID { get; set; }
        public int CoachId { get; set; }

        [Display(Name = "Max members")]
        public int GroupMaxMembers { get; set; }

        public CoachGroupDetailViewModel Coach { get; set; }

        public IEnumerable<ClientViewModel> Clients { get; set; }

        public string Sport { get; set; }
    }

    public class GroupEditViewModel {
        public int ID { get; set; }
        public int CoachId { get; set; }

        [Display(Name = "Max members")]
        public int GroupMaxMembers { get; set; }
    }

    public class GroupSheduleViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Coach")]
        public string CoachFullName { get; set; }

        public string Sport { get; set; }
    }

    public class GroupListViewModel {
        public int ID { get; set; }
        public int CoachId { get; set; }
        public int SportId { get; set; }

        [Display(Name = "Coach")]
        public string CoachFullName { get; set; }

        [Display(Name = "Max members")]
        public int GroupMaxMembers { get; set; }

        public string Sport { get; set; }
    }

    public class CreateGroupViewModel
    {
        public int CoachId { get; set; }
        public int SportId { get; set; }
        public int GroupMaxMembers { get; set; }
        public int SheduleDayId { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan SheduleTime { get; set; }
        public int HallId { get; set; }
    }
}