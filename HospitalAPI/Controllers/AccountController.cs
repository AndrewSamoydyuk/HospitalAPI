using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using HospitalAPI.Models;
using HospitalAPI.Providers;
using HospitalAPI.App_Start;
using HospitalAPI.Filters;
using HospitalAPI.DALs;
using HospitalAPI.Results;
using System.Web.Http.Cors;

namespace HospitalAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private IPatientRepository patientRepository;

        public AccountController()
        {
            this.patientRepository = new PatientRepository(new HospitalContext());
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }


        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        [ValidateModel]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [ValidateModel]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            var patient = new Patient() {
                FullName = model.FullName,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Sex = model.Sex,
                Phone = model.Phone,
                ImageUri = "DefaultImageForPatient.jpg"
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            UserManager.AddToRole(user.Id, "Patient");
            patient.ApplicationUserID = user.Id;

            patientRepository.AddPatient(patient);
            patientRepository.Save();

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

         protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

    }
}

