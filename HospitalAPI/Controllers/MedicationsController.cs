using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.DALs;
using HospitalAPI.DTOs;
using HospitalAPI.Models;
using System.Web.Http.Description;

namespace HospitalAPI.Controllers
{
    public class MedicationsController : ApiController
    {
        private IMedicationRepository medicationRepository;

        public MedicationsController()
        {
            this.medicationRepository = new MedicationRepository(new HospitalContext());
        }

        public MedicationsController(IMedicationRepository medicationRepository)
        {
            this.medicationRepository = medicationRepository;
        }

        // GET api/medications
        [HttpGet]
        public IEnumerable<MedicationDTO> GetMedications()
        {
            return medicationRepository.GetMedications();
        }

        //GET api/medications/1
        [HttpGet]
        [ResponseType(typeof(MedicationDTO))]
        public IHttpActionResult GetMedication(int id)
        {
            var medication = medicationRepository.MedicationDetails(id);

            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        //POST api/medication
        [HttpPost]
        [ResponseType(typeof(Medication))]
        public IHttpActionResult PostMedication([FromBody]Medication medication)
        {
            if (ModelState.IsValid)
            {
                medicationRepository.AddMedication(medication);
                medicationRepository.Save();

                return CreatedAtRoute("DefaultApi", new {id = medication.Id }, medication);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ResponseType(typeof(Medication))]
        public IHttpActionResult DeleteMedication(int id)
        {
            var medication = medicationRepository.GetMedicationById(id);
            if (medication == null)
            {
                return NotFound();
            }

            medicationRepository.DeleteMedication(medication);
            medicationRepository.Save();

            return Ok(medication);
     
        }

    }
}
