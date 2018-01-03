using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAPI.DTOs
{
    public class DoctorDetailDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Education { get; set; }

        public string Sex { get; set; }

        public string Speciality { get; set; }

        public string ImageUri { get; set; }

        public int RoomNumber { get; set; }

        public DepartmentDTO Department { get; set; }

        public IEnumerable<ScheduleDTO> Schedule { get; set; }
    }
}