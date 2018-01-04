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

        //PUT api/patients/1
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatient(int id, [FromBody]Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }else if (patientRepository.GetPatientById(id) == null)
            {
                return NotFound();
            }

            patientRepository.UpdatePatient(patient);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/patients
        [HttpPost]
        [ResponseType(typeof(PatientDTO))]
        public IHttpActionResult PostPatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            patientRepository.AddPatient(patient);
            patientRepository.Save();

            var patientDTO = new PatientDTO()
            {
                id = patient.Id,
                Address = patient.Address,
                FullName = patient.FullName
            };

            return CreatedAtRoute("DefaultApi", new { id = patient.Id }, patientDTO);
        }

        // DELETE api/patients/1
        [HttpDelete]
        public IHttpActionResult DeletePatient(int id)
        {
            var patient = patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }

            patientRepository.DeletePatient(patient);
            patientRepository.Save();

            return Ok(patient);
        }
    }
}
