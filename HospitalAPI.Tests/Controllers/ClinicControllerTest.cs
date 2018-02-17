using HospitalAPI.Controllers;
using System.Web.Mvc;
using Xunit;
using Moq;
using HospitalAPI.Models;
using System.Web;
using HospitalAPI.DALs;
using HospitalAPI.DTOs;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Data.Entity.Core;
using System.Web.Configuration;
using System.Web.Http.Results;

namespace HospitalAPI.Tests.Controllers
{
    public class ClinicControllerTest
    {
        [Fact]
        public void GetClinicsIsCorrect()
        {
            var mockRepository = new Mock<IClinicRepository>();
            mockRepository.Setup(c => c.GetClinics()).Returns(new List<ClinicDTO> { new ClinicDTO { Id = 1, Address = "Address", ImageUri = "Uri", Name = "Name" } });
            ClinicsController controller = new ClinicsController(mockRepository.Object);
            List<ClinicDTO> res =  controller.GetClinics() as List<ClinicDTO>;

            Assert.Equal(1, res[0].Id);
        }

        [Fact]
        public void GetClinicNotFoundExpectedIsCorrect()
        {
            var mockRepository = new Mock<IClinicRepository>();
            ClinicsController controller = new ClinicsController(mockRepository.Object);
            Assert.Throws<HttpResponseException>(() => controller.GetClinic(1));
        }

        [Fact]
        public void GetClinicIsCorrect()
        {
            var mockRepository = new Mock<IClinicRepository>();
            mockRepository.Setup(c => c.ClinicDetails(1)).Returns(new ClinicDetailDTO { Id = 1 });

            ClinicsController controller = new ClinicsController(mockRepository.Object);
            IHttpActionResult result = controller.GetClinic(1);

            var contentResult = result as OkNegotiatedContentResult<ClinicDetailDTO>;

            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(1, contentResult.Content.Id);
        }

    }
}
