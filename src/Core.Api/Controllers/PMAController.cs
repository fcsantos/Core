using Asp.Versioning;
using Core.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pma")]
    public class PMAController : MainController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PMAController(IHttpClientFactory httpClientFactory, INotifier notifier, IUser user) : 
            base(notifier, user) => 
            _httpClientFactory = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient("Alpha");

            var response = await client.GetAsync("program_manager_accounts/projetoalpha_pma_postpaid_1");

            if (response.IsSuccessStatusCode)
            {
                PMAResponse pMA = JsonConvert.DeserializeObject<PMAResponse>(response.Content.ReadAsStringAsync().Result);
                return Ok(pMA);
            }
            else
            {
                return StatusCode(500, "Something Went Wrong! Error Occured");
            }
        }
    }
}
public class PMAResponse
{
    public long added_time { get; set; }
    public int balance { get; set; }
    public int currency { get; set; }
    public string id { get; set; }
    public string issuer_id { get; set; }
    public bool rtf_account { get; set; }
    public string status { get; set; }
}