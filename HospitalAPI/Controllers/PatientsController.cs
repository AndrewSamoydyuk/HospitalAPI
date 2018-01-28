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
                return NotFound();
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
        [Authorize(Roles = "Admin, Doctor")]
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
        [Authorize(Roles = "Admin, Doctor")]
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
                return NotFound();
            }

            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/" + patient.ImageUri)))
            {
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/" + patient.ImageUri));
            }

            var provider = new MultipartMemoryStreamProvider();
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/");
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                byte[] fileArray = await file.ReadAsByteArrayAsync();

                using (System.IO.FileStream fs = new System.IO.FileStream(root + filename, System.IO.FileMode.Create))
                {
                    await fs.WriteAsync(fileArray, 0, fileArray.Length);
                }
                patient.ImageUri = "~/Content/Images/" + filename;
            }
            patientRepository.UpdatePatient(patient);
            patientRepository.Save();

            return Ok();
        }

        //POST api/patients/id/addvisit
        [HttpPost]
        [Authorize(Roles = "Admin, Doctor")]
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
        [Authorize(Roles = "Admin, Doctor")]
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
        [Authorize(Roles = "Admin, Doctor")]
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
        [Authorize(Roles = "Admin, Doctor")]
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
