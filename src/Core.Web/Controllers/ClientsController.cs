using Core.Web.Extensions;
using Core.Web.Helpers;
using Core.Web.Models;
using Core.Web.Resource;
using Core.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Controllers
{
    public class ClientsController : MainController
    {
        private readonly IClientService _clientService;
        private readonly IStringLocalizer<ClientsController> _localizer;
        private readonly IStringLocalizer<Resources> _localizerGeneral;
        private readonly IAccountService _accountService;
        private readonly AppSettings _appSettings;
        private readonly SymmetricEncryptDecrypt _symmetricEncryptDecrypt;
        private readonly IAspNetUser _aspNetUser;
        private readonly IFileProvider _fileProvider;


        public ClientsController(IClientService clientService,
                                 IStringLocalizer<ClientsController> localizer,
                                 IStringLocalizer<Resources> localizerGeneral,
                                 IAccountService accountService,
                                 IOptions<AppSettings> appSettings,
                                 SymmetricEncryptDecrypt symmetricEncryptDecrypt,
                                 IFileProvider fileProvider,
                                 IAspNetUser aspNetUser)
        {
            _clientService = clientService;
            _localizerGeneral = localizerGeneral;
            _localizer = localizer;
            _accountService = accountService;
            _appSettings = appSettings.Value;
            _symmetricEncryptDecrypt = symmetricEncryptDecrypt;
            _aspNetUser = aspNetUser;
            _fileProvider = fileProvider;
        }

        [ClaimsAuthorize("Client", "Get")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _clientService.GetAll());
        }

        [ClaimsAuthorize("Client", "Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Client", "Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            clientViewModel.CertificatePathPfx = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\certificates", clientViewModel.CertificatePathPfx);
            clientViewModel.CertificatePathCer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\certificates", clientViewModel.CertificatePathCer);

            var password = string.Empty;
#if DEBUG
            password = _appSettings.DefaultPassword;
#else
                password = PasswordGenerator.GenerateRandomPassword();
#endif

            var (Key, IVBase64) = _symmetricEncryptDecrypt.InitSymmetricEncryptionKeyIV();

            clientViewModel.SecretKey = Key;
            clientViewModel.IVBase64 = IVBase64;

            var userRegister = new UserRegister { Email = clientViewModel.Email, Password = password, ConfirmPassword = password, Role = _appSettings.RoleClient, Name = clientViewModel.Name };
            var responseAuth = await _accountService.RegisterUser(userRegister);

            if (ResponseHasErrors(responseAuth.ResponseResult))
            {
                return View(clientViewModel);
            }
            else
            {
                clientViewModel.UserId = responseAuth.UserToken.Id;

                Dictionary<string, string> client = new Dictionary<string, string>();
                client.Add("Email", clientViewModel.Email);
                client.Add("UserId", clientViewModel.UserId);
                client.Add("SecretKey", clientViewModel.SecretKey);
                client.Add("IVBase64", clientViewModel.IVBase64);

                clientViewModel.ApiKey = _symmetricEncryptDecrypt.Encrypt(string.Join(",", client), IVBase64, Key);

                var response = await _clientService.Create(clientViewModel);

                if (ResponseHasErrors(response))
                {
                    await _clientService.DeleteUser(responseAuth.UserToken.Id);

                    return View(clientViewModel);
                }
            }

            return RedirectToAction("Index", "Clients");
        }

        [ClaimsAuthorize("Client", "Update")]
        [HttpGet("{id:guid}/EditPatient")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var clientViewModel = await _clientService.GetById(id);

            if (clientViewModel == null) return NotFound();

            ViewData["EditClientName"] = _localizer["Editar Cliente: {0}", clientViewModel.NIF + " - " + clientViewModel.Name];

            return View(clientViewModel);
        }

        [ClaimsAuthorize("Client", "Update")]
        [HttpPost]
        public async Task<IActionResult> EditPost(ClientViewModel clientViewModel)
        {
            clientViewModel.CertificatePathPfx = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\certificates", clientViewModel.CertificatePathPfx);
            clientViewModel.CertificatePathCer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\certificates", clientViewModel.CertificatePathCer);

            var result = await _clientService.GetById(clientViewModel.Id);
            clientViewModel.IVBase64 = result.IVBase64;
            clientViewModel.SecretKey = result.SecretKey;
            clientViewModel.ApiKey = result.ApiKey;
            clientViewModel.UserId = result.UserId;

            var response = await _clientService.Update(clientViewModel);

            if (ResponseHasErrors(response)) TempData["Erros"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

            return RedirectToAction("Index", "Clients");
        }

        [ClaimsAuthorize("Client", "Delete")]
        [HttpPost]
        public ActionResult Delete(Guid Id)
        {
            var patientViewModel = _clientService.GetById(Id).Result;

            if (patientViewModel == null) return NotFound();

            var response = _clientService.Delete(Id).Result;

            if (response.Errors.Messages.Any())
                return Json(new DeleteResponseMessage { message = response.Errors.Messages.FirstOrDefault(), success = false });

            return Json(new DeleteResponseMessage { message = patientViewModel.IsActive.Value ? _localizerGeneral["Registro inativado com sucesso"] : _localizerGeneral["Registro ativado com sucesso"], success = true });
        }

        [ClaimsAuthorize("Client", "GetClient")]
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail()
        {
            var clientViewModel = await _clientService.GetByUserId(_aspNetUser.GetUserId());

            if (clientViewModel == null) return NotFound();

            return View(clientViewModel);
        }

        [ClaimsAuthorize("Client", "Update")]
        [HttpPost]
        public async Task<IActionResult> DetailPost()
        {
            var clientViewModel = await _clientService.GetByUserId(_aspNetUser.GetUserId());

            if (clientViewModel == null) return NotFound();

            var (Key, IVBase64) = _symmetricEncryptDecrypt.InitSymmetricEncryptionKeyIV();

            clientViewModel.SecretKey = Key;
            clientViewModel.IVBase64 = IVBase64;

            Dictionary<string, string> client = new Dictionary<string, string>();
            client.Add("Email", clientViewModel.Email);
            client.Add("UserId", clientViewModel.UserId);
            client.Add("SecretKey", clientViewModel.SecretKey);
            client.Add("IVBase64", clientViewModel.IVBase64);

            clientViewModel.ApiKey = _symmetricEncryptDecrypt.Encrypt(string.Join(",", client), IVBase64, Key);

            var response = await _clientService.Update(clientViewModel);

            if (ResponseHasErrors(response)) TempData["Erros"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

            return RedirectToAction("Detail", "Clients");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\certificates",
                        file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Index", "Clients");
        }

        [ClaimsAuthorize("Client", "Update")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Files(Guid id)
        {
            var clientViewModel = await _clientService.GetById(id);

            if (clientViewModel == null) return NotFound();


            return View();
        }
    }
}
