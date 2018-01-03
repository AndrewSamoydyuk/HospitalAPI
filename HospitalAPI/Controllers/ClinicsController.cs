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
using System.Data.Entity.Infrastructure;

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

        // GET api/Clinics
        [HttpGet]
        public IEnumerable<ClinicDTO> GetClinics()
        {
            return clinicRepository.GetClinics();
        }

        // GET api/Clinics/1
        [HttpGet]
        [ResponseType(typeof(ClinicDetailDTO))]
        public IHttpActionResult GetClinic(int id)
        {
            var clinic = clinicRepository.ClinicDetails(id);

            if (clinic == null)
            {
                return NotFound();
            }

            return Ok(clinic);
        }

        // PUT api/Clinic/1
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClinic(int id, [FromBody]Clinic clinic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (clinicRepository.GetClinicById(id) == null)
            {
                return NotFound();
            }

            clinicRepository.UpdateClinic(clinic);
            clinicRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Clinics
        [HttpPost]
        [ResponseType(typeof(ClinicDTO))]
        public IHttpActionResult PostClinic([FromBody]Clinic clinic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            clinicRepository.AddClinic(clinic);
            clinicRepository.Save();

            var ClinicDTO = new ClinicDTO()
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Address = clinic.Address,
                ImageUri = clinic.ImageUri
            };

            return CreatedAtRoute("DefaultApi", new { id = clinic.Id }, ClinicDTO );
        }

        // DELETE api/clinics/1
        [ResponseType(typeof(Clinic))]
        [HttpDelete]
        public IHttpActionResult DeleteClinic(int id)
        {
            var clinic = clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                return NotFound();
            }

            clinicRepository.DeleteClinic(clinic);
            clinicRepository.Save();

            return Ok(clinic);
        }

        [HttpPost]
        [Route("~/api/clinics/addDepartment")]
        public IHttpActionResult PostDepartment([FromBody]Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            clinicRepository.AddDepartment(department);
            clinicRepository.Save();

            return StatusCode(HttpStatusCode.Created);

        }

    }
}
