using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class SchedulePerDay
    {
        public int Id { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [Required]
        public TimeSpan TimeStart { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        [Required]
        public TimeSpan TimeEnd { get; set; }

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

    }

    public enum DayOfWeek
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}