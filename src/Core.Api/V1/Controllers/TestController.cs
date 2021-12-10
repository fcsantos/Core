using Core.Api.Controllers;
using Core.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Core.Api.V1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/test")]
    public class TestController : MainController
    {
        private readonly IConfiguration _configuration;

        public TestController(INotifier notifier, IUser appUser, IConfiguration configuration) : base(notifier, appUser)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string Value()
        {
            var claims = _configuration.GetSection("ClaimsList").Get<Dictionary<string,string[]>>();

            return "Sou a V1";
        }
    }
}