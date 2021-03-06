//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using Core.Api.Extensions;
//using Core.Api.ViewModels;
//using Core.Business.Intefaces;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.Extensions.Logging;
//using System.Text.Encodings.Web;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.WebUtilities;


using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using Core.Api.Extensions;
using Core.Api.ViewModels;
using Core.Business.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using AutoMapper;

namespace Core.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user,
                              ILogger<AuthController> logger, 
                              IEmailSender emailSender, 
                              IMapper mapper, 
                              RoleManager<IdentityRole> roleManager) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _logger = logger;
            _emailSender = emailSender;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarJwt(registerUser.Email));
            }
            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("esqueci-minha-senha")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                NotificarErro("Se você possui uma conta conosco, enviamos um e-mail com as instruções para redefinir sua senha.");
                return CustomResponse(input);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = string.Format("{0}?code={1}", _appSettings.Url,code);

            await _emailSender.SendEmailAsync(
                input.Email,
                "Redefinir senha",
                $"Redefina sua senha clicando no link: { HtmlEncoder.Default.Encode(callbackUrl)}");

            return CustomResponse(input);
        }

        [HttpPost("redefinir-senha")]
        public async Task<ActionResult<ResetPasswordViewModel>> ResetPassword(ResetPasswordViewModel input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
            {
                NotificarErro("Se você possui uma conta conosco, enviamos um e-mail com as instruções para redefinir sua senha.");
                return CustomResponse(input);
            }

            var result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(input.Code)), input.Password);
            if (result.Succeeded)
            {
                return CustomResponse("Senha alterada com sucesso.");
            }
            else
            {
                NotificarErro(result.Errors.FirstOrDefault().Description);
                return CustomResponse(input);
            }

            return CustomResponse(input);
        }


        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuário " + loginUser.Email + " logado com sucesso");
                return CustomResponse(await GerarJwt(loginUser.Email));
            }
            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou Senha incorretos");
            return CustomResponse(loginUser);
        }

        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            var encondedToken = tokenHandler.WriteToken(token);

            return encondedToken;
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds, //posso colocar dentro de um timer na minha aplicação
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        [Authorize(Roles = "admin")]
        [HttpGet("roles")]
        public async Task<IEnumerable<RoleViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<RoleViewModel>>(_roleManager.Roles);
        }
    }
}