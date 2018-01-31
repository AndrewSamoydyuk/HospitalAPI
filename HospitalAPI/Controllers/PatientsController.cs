using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.DTOs;
using HospitalAPI.DALs;
using HospitalAPI.Helpers;
using HospitalAPI.Models;
using HospitalAPI.Filters;
using System.Web.Http.Description;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Admin, Doctor")]
        public IEnumerable<PatientDTO> GetPatients()
        {
            return patientRepository.GetPatients();
        }

        //GET api/patients/getVisitsByDate/date
        [HttpGet]
        [Authorize(Roles = "Admin, Doctor")]
        [Route("~/api/patients/getVisitsByDate/{*date:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")] // * - span several URI segments
        public IEnumerable<VisitDTO> GetVisitsByDate(DateTime date)
        {
            return patientRepository.GetVisitsByDate(date);
        }

        // GET api/patient/1
        [HttpGet]
        [Authorize(Roles = "Admin, Doctor, Patient")]
        [ResponseType(typeof(PatientDetailDTO))]
        public IHttpActionResult GetPatient(int id)
        {
            var patient = patientRepository.GetPatientDetails(id);

            if (patient == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No patient with id - {id}")
                });
            }

            if (User.IsInRole("Patient"))
            {
                if (User.Identity.Name == patient.UserName)
                {
                    return Ok(patient);
                }
                else
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                return Ok(patient);
            }

        }

        //PUT api/patients/1
        [HttpPut]
        [ValidateModel]
        [Authorize(Roles = "Admin, Doctor")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatient(int id, [FromBody]Patient patient)
        {
            if (patientRepository.GetPatientById(id) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No patient with id - {id}")
                });
            }

            patientRepository.UpdatePatient(patient);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //PUT api/patients/visitId/changeStatus
        [HttpPut]
        [Authorize(Roles = "Admin, Doctor")]
        [Route("~/api/patients/{visitId:int}/changeStatus")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeStatus(int visitId, [FromBody]Status status)
        {
            var visit = patientRepository.GetVisitById(visitId);
            if (visit == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No visit with id - {visitId}")
                });
            }

            visit.Status = status;
            patientRepository.UpdateVisitStatus(visit);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.OK);
        }


        //POST api/patients/1/addImage
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize(Roles = "Admin, Doctor")]
        [Route("~/api/patients/{id:int}/updateImage")]
        public async Task<IHttpActionResult> UpdateImage(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var patient = patientRepository.GetPatientById(id);

            if (patient == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No patient with id - {id}")
                });
            }

            ImageHandler.DeleteImageIfExist(patient.ImageUri);
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            patient.ImageUri = await ImageHandler.UploadImage(provider.Contents.FirstOrDefault());

            patientRepository.UpdatePatient(patient);
            patientRepository.Save();

            return Ok();
        }

        //POST api/patients/id/addvisit
        [HttpPost]
        [Authorize(Roles = "Admin, Doctor")]
        [ValidateModel]
        [Route("~/api/patients/{id:int}/addvisit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddVisit(int id, [FromBody]PatientVisit visit)
        {
            var patient = patientRepository.GetPatientById(id);
            if (patient == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No patient with id - {id}")
                });
            }

            visit.PatientID = patient.Id;
            visit.Status = Status.OnTreatment;
            patientRepository.AddVisit(visit);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.Created);
        }

        // POST api/patients/visitId/addMedication
        [Authorize(Roles = "Admin, Doctor")]
        [ValidateModel]
        [Route("~/api/patients/{visitId:int}/addMedication")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddMedication(int visitId, [FromBody] PatientVisitMedication medication)
        {
            var visit = patientRepository.GetVisitById(visitId);
            if (visit == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No visit with id - {visitId}")
                });
            };

            medication.PatientVisitID = visitId;
            patientRepository.AddMedication(medication);
            patientRepository.Save();

            return StatusCode(HttpStatusCode.Created);
        }

        // POST api/patients
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin, Doctor")]
        [ResponseType(typeof(PatientDTO))]
        public IHttpActionResult PostPatient([FromBody] Patient patient)
        {
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
        [Authorize(Roles = "Admin, Doctor")]
        public IHttpActionResult DeletePatient(int id)
        {
            var patient = patientRepository.GetPatientById(id);
            if (patient == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No patient with id - {id}")
                });
            }

            patientRepository.DeletePatient(patient);
            patientRepository.Save();

            return Ok(patient);
        }
    }
}
