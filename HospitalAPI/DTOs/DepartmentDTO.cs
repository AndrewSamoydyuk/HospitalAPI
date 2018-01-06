﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAPI.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ResultOfTreatment ResultsOfTreatment { get; set; }
    }
}