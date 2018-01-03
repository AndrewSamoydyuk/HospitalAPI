using System;
using System.Collections.Generic;
using System.Linq;
using HospitalAPI.Models;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HospitalAPI.DTOs
{
    public class ScheduleDTO
    {
        public int Id { get; set; }

        public string TimeStart { get; set; }

        public string TimeEnd { get; set; }
        
        public DayNumber DayNumber { get; set; }
    }
}