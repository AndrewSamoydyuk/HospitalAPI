using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
        public DayNumber DayNumber { get; set; }

    }

    public enum DayNumber
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
}