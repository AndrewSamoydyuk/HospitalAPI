using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAPI.DTOs
{
    public class MedicatonDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AveragePrice { get; set; }

        public string Description { get; set; }

        public int UsedTimes { get; set; }

        public int UsedInThisMonthTimes { get; set; }

    }
}