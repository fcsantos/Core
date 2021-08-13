using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Core.Api.Extensions;
using Core.Business.Intefaces;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Core.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/jarvis")]
    public class JarvisController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly AppSettings _appSettings;

        public JarvisController(UserManager<IdentityUser> userManager,
                                IEmailSender emailSender,
                                IOptions<AppSettings> appSettings,
                                INotifier notifier, IUser user) : base(notifier, user)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _appSettings = appSettings.Value;
        }

        //[HttpGet("send-access-new-patient")]
        //public async Task<ActionResult> SendAccessToNewPatient()
        //{
        //    var password = PasswordGenerator.GenerateRandomPassword();

        //    var patients = await _patientRepository.GetPatientIsMailNotSender();

        //    foreach (var patient in patients)
        //    {
        //        var user = await _userManager.FindByEmailAsync(patient.Email);
        //        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //        {
        //            return CustomResponse("Email " + patient.Email + " não encontrado");
        //        }

        //        var result = await _userManager.ChangePasswordAsync(user, _appSettings.DefaultPassword, password);
        //        if (!result.Succeeded)
        //        {
        //            return CustomResponse(result.Errors.FirstOrDefault());
        //        }

        //        await _emailSender.SendEmailAsync(
        //            string.Format("{0},{1}", patient.FirstName + " " + patient.LastName, patient.Email),
        //            "Utilizador cadastrado",
        //            $"Favor aceda com o link: { HtmlEncoder.Default.Encode(_appSettings.UrlLogin)} " + " com a password " + password);
                
        //        await _patientService.UpdateEmailSender(patient);
        //    }

        //    return CustomResponse("Emails enviados com sucesso");
        //}
    }
}
