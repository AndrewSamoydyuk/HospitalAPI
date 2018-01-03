using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.DTOs;
using HospitalAPI.DALs;
using HospitalAPI.Models;
using System.Web.Http.Description;

namespace HospitalAPI.Controllers
{
    public class PatientsController : ApiController
    {
        private IPatientRepository patientRepository;

        public PatientsController()
        {
            this.patientRepository = new PatientRepository(new HospitalContext());
        }

        public PatientsController(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        // GET api/patients
        [HttpGet]
        public IEnumerable<PatientDTO> GetPatients()
        {
            return patientRepository.GetPatients();
        }

        // GET api/patient/1
        [HttpGet]
        [ResponseType(typeof(PatientDetailsDTO))]
        public IHttpActionResult GetPatient(int id)
        {
            var patient = patientRepository.GetPatientDetails(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}
