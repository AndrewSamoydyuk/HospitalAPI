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

        //GET api/patients/getVisitsByDate/date
        [HttpGet]
        [Route("~/api/patients/getVisitsByDate/{*date:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")] // * - span several URI segments
        public IEnumerable<VisitDTO> GetVisitsByDate(DateTime date)
        {
            return patientRepository.GetVisitsByDate(date);
        }

        // GET api/patient/1
        [HttpGet]
        [ResponseType(typeof(PatientDetailDTO))]
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

        //PUT api/patients/visitId/changeStatus
        [HttpPut]
        [Route("~/api/patients/{visitId:int}/changeStatus")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeStatus(int visitId, [FromBody]Status status)
        {
            var visit = patientRepository.GetVisitById(visitId);
            if (visit == null)
            {
                return NotFound();
            }

            visit.Status = status;
            patientRepository.UpdateVisitStatus(visit);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST api/patients/id/addvisit
        [HttpPost]
        [Route("~/api/patients/{id:int}/addvisit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddVisit(int id,[FromBody]PatientVisit visit)
        {
            var patient = patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }

            visit.PatientID = patient.Id;
            visit.Status = Status.OnTreatment;
            patientRepository.AddVisit(visit);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.Created);
        }

        // POST api/patients/visitId/addMedication
        [Route("~/api/patients/{visitId:int}/addMedication")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddMedication(int visitId, [FromBody] PatientVisitMedication medication)
        {
            var visit = patientRepository.GetVisitById(visitId);
            if (visit == null)
            {
                return NotFound();
            };

            medication.PatientVisitID = visitId;
            patientRepository.AddMedication(medication);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.Created);
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
