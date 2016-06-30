using System;

namespace SportLife.Website.Areas.AdminOffice.Models {
    public class SheduleAdminViewModel {
        public int ShedulId { get; set; }
        public string SheduleDay { get; set; }
        public DateTime SheduleTime { get; set; }
        public GroupSheduleViewModel Group { get; set; }
        public int HallId { get; set; }
    }

    public class SheduleEditViewModel
    {
        public int ShedulId { get; set; }
        public string SheduleDay { get; set; }
        public int SheduleDayId { get; set; }
        public TimeSpan SheduleTime { get; set; }
    }
}