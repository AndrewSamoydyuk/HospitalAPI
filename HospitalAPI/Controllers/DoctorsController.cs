using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.Models;
using HospitalAPI.DALs;
using HospitalAPI.DTOs;
using HospitalAPI.Helpers;
using HospitalAPI.Filters;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{

    public class DoctorsController : ApiController
    {
        private IDoctorRepository doctorRepository;

        public DoctorsController()
        {
            this.doctorRepository = new DoctorRepository(new HospitalContext());
        }

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        //GET api/doctors
        [HttpGet]
        [Authorize(Roles = "Admin, Doctor")]
        public IEnumerable<DoctorDTO> GetDoctors()
        {
            return doctorRepository.GetDoctors();
        }

        // GET api/doctors/getByWorkDay/Monday
        [HttpGet]
        [Route("~/api/doctors/getByWorkDay/{day}")]
        [Authorize(Roles = "Admin, Doctor")]
        public IEnumerable<DoctorDTO> GetByWorkDay(string day)
        {
            return doctorRepository.GetDoctorsByWorkDay(day);           
        }

        //GET api/doctors/1
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        [ResponseType(typeof(DoctorDetailDTO))]
        public IHttpActionResult GetDoctor(int id)
        {
            var doctor = doctorRepository.DoctorDetails(id);

            if (doctor == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No doctor with id - {id}")
                });
            }

            return Ok(doctor);
        }

        //PUT api/doctors/1
        [HttpPut]
        [ResponseType(typeof(void))]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutDoctor(int id, [FromBody]Doctor doctor)
        {
            if (doctorRepository.GetDoctorById(id) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No doctor with id - {id}")
                });
            }

            doctorRepository.UpdateDoctor(doctor);
            doctorRepository.Save();

            return StatusCode(HttpStatusCode.OK);
        }

        // POST api/doctors
        [HttpPost]
        [ResponseType(typeof(DoctorDTO))]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostDoctor([FromBody]Doctor doctor)
        { 
            doctorRepository.AddDoctor(doctor);
            doctorRepository.Save();

            var DoctorDTO = new DoctorDTO()
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Speciality = doctor.Speciality
            };

            return CreatedAtRoute("DefaultAPI", new { id = doctor.Id }, DoctorDTO);
        }


        //POST api/doctors/1/addImage
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("~/api/doctors/{id:int}/updateImage")]
        public async Task<IHttpActionResult> UpdateImage(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var doctor = doctorRepository.GetDoctorById(id);

            if (doctor == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No doctor with id - {id}")
                });
            }

            ImageHandler.DeleteImageIfExist(doctor.ImageUri);
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            doctor.ImageUri = await ImageHandler.UploadImage(provider.Contents.FirstOrDefault());

            doctorRepository.UpdateDoctor(doctor);
            doctorRepository.Save();

            return Ok();
        }

        // DELETE api/doctors/1
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteDoctor(int id)
        {
            var doctor = doctorRepository.GetDoctorById(id);
            if (doctor == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No doctor with id - {id}")
                });
            }

            doctorRepository.DeleteDoctor(doctor);
            doctorRepository.Save();

            return Ok(doctor);
        }
    }
}
