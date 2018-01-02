using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.DALs;
using HospitalAPI.Models;
using System.Web.Http.Description;
using HospitalAPI.DTOs;

namespace HospitalAPI.Controllers
{
    public class ClinicsController : ApiController
    {
        private IClinicRepository clinicRepository;

        public ClinicsController()
        {
            this.clinicRepository = new ClinicRepository(new HospitalContext());
        }

        public ClinicsController(IClinicRepository clinicRepository)
        {
            this.clinicRepository = clinicRepository;
        }

        //GET api/Clinics
        public IEnumerable<ClinicDTO> GetClinics()
        {
            return clinicRepository.GetClinics();
        }

        // GET api/Clinics/1
        [ResponseType(typeof(ClinicDetailDTO))]
        public IHttpActionResult GetClinic(int id)
        {
            var clinic = clinicRepository.GetClinicById(id);

            if (clinic == null)
            {
                return NotFound();
            }

            return Ok(clinic);
        }
    }
}
