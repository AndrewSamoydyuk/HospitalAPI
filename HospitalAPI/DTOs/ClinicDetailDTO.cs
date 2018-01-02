using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAPI.DTOs
{
    public class ClinicDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ImageUri { get; set; }

        public int CountOfDepartments { get; set; }

        public List<DepartmentDTO> Departments { get; set; }
    }
}