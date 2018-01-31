using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.DALs;
using HospitalAPI.DTOs;
using HospitalAPI.Filters;
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
        [Authorize]
        public IEnumerable<MedicationDTO> GetMedications()
        {
            return medicationRepository.GetMedications();
        }

        //GET api/medications/1
        [HttpGet]
        [ResponseType(typeof(MedicationDTO))]
        [Authorize]
        public IHttpActionResult GetMedication(int id)
        {
            var medication = medicationRepository.MedicationDetails(id);

            if (medication == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No medication with id - {id}")
                });
            }

            return Ok(medication);
        }

        //POST api/medication
        [HttpPost]
        [ValidateModel]
        [ResponseType(typeof(Medication))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostMedication([FromBody]Medication medication)
        {
            medicationRepository.AddMedication(medication);
            medicationRepository.Save();

            return CreatedAtRoute("DefaultApi", new {id = medication.Id }, medication);
        }

        // DELETE api/medications/1
        [HttpDelete]
        [ResponseType(typeof(Medication))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteMedication(int id)
        {
            var medication = medicationRepository.GetMedicationById(id);
            if (medication == null)
            {
                if (medication == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent($"No medication with id - {id}")
                    });
                }
            }

            medicationRepository.DeleteMedication(medication);
            medicationRepository.Save();

            return Ok(medication);
     
        }

    }
}
