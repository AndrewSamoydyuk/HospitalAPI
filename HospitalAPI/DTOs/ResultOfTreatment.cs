using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAPI.DTOs
{
    public class ResultOfTreatment
    {
        public int CountOfCured { get; set; }

        public int CountOfNotCured { get; set; }

        public int CountOfOnTreatment { get; set; }
    }
}