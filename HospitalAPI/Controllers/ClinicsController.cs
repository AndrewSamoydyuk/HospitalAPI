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
using HospitalAPI.Filters;
using HospitalAPI.Helpers;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [AllowAnonymous]
        public IEnumerable<ClinicDTO> GetClinics()
        {
            return clinicRepository.GetClinics();
        }

        // GET api/Clinics/1
        [HttpGet]
        [AllowAnonymous]
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
        [ValidateModel]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClinic(int id, [FromBody]Clinic clinic)
        {
            if (clinicRepository.GetClinicById(id) == null)
            {
                return NotFound();
            }

            clinicRepository.UpdateClinic(clinic);
            clinicRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Clinics
        [HttpPost]
        [ValidateModel]
        [ResponseType(typeof(ClinicDTO))]
        public IHttpActionResult PostClinic([FromBody]Clinic clinic)
        {
            clinicRepository.AddClinic(clinic);
            clinicRepository.Save();

            var clinicDTO = new ClinicDTO()
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Address = clinic.Address,
                ImageUri = clinic.ImageUri
            };

            return CreatedAtRoute("DefaultApi", new { id = clinic.Id }, clinicDTO);
        }


        //POST api/clinics/1/updateImage
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("~/api/clinics/{id:int}/updateImage")]
        public async Task<IHttpActionResult> UpdateImage(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var clinic = clinicRepository.GetClinicById(id);

            if (clinic == null)
            {
                return NotFound();
            }

            //use helper for handling images
            ImageHandler.DeleteImageIfExist(clinic.ImageUri);
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            clinic.ImageUri = await ImageHandler.UploadImage(provider.Contents.FirstOrDefault());

            clinicRepository.UpdateClinic(clinic);
            clinicRepository.Save();

            return Ok();
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


        //POST api/clinics/addDepartment
        [HttpPost]
        [ValidateModel]
        [Route("~/api/clinics/addDepartment")]
        public IHttpActionResult PostDepartment([FromBody]Department department)
        {
            clinicRepository.AddDepartment(department);
            clinicRepository.Save();

            return StatusCode(HttpStatusCode.Created);

        }
    }
}
