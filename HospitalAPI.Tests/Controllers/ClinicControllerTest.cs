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
using System.Web.Http.Results;
using System.Web.Http;

namespace HospitalAPI.Tests.Controllers
{
    public class ClinicControllerTest
    {
        [Fact]
        public void ClinicNotFoundIsRight()
        {
            var mockRepository = new Mock<IClinicRepository>();
            //mockRepository.Setup(c => c.ClinicDetails(1)).Returns(new ClinicDetailDTO());
            ClinicsController controller = new ClinicsController(mockRepository.Object);
            IHttpActionResult result = controller.GetClinic(1);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
