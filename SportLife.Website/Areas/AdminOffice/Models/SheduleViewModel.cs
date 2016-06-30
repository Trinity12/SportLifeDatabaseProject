using System;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Time)]
        public TimeSpan SheduleTime { get; set; }
    }

    public class DayInWeekDropDown
    {
        public int DayId { get; set; }
        public string DayString { get; set; }
    }
}