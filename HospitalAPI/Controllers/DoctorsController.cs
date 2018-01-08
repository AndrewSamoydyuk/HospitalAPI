using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.Models;
using HospitalAPI.DALs;
using HospitalAPI.DTOs;
using System.Web.Http.Description;

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
        public IEnumerable<DoctorDTO> GetDoctors()
        {
            return doctorRepository.GetDoctors();
        }

        // GET api/doctors/getByWorkDay/Monday
        [HttpGet]
        [Route("~/api/doctors/getByWorkDay/{day}")]
        public IEnumerable<DoctorDTO> GetByWorkDay(string day)
        {
            return doctorRepository.GetDoctorsByWorkDay(day);
            
        }

        //GET api/doctors/1
        [HttpGet]
        [ResponseType(typeof(DoctorDetailDTO))]
        public IHttpActionResult GetDoctor(int id)
        {
            var doctor = doctorRepository.DoctorDetails(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        //PUT api/doctors/1
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDoctor(int id, [FromBody]Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (doctorRepository.GetDoctorById(id) == null)
            {
                return NotFound();
            }

            doctorRepository.UpdateDoctor(doctor);
            doctorRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/doctors
        [HttpPost]
        [ResponseType(typeof(DoctorDTO))]
        public IHttpActionResult PostDoctor([FromBody]Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

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

        // DELETE api/doctors/1
        [HttpDelete]
        public IHttpActionResult DeleteDoctor(int id)
        {
            var doctor = doctorRepository.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctorRepository.DeleteDoctor(doctor);
            doctorRepository.Save();

            return Ok(doctor);
        }
    }
}
