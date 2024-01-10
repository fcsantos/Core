using Asp.Versioning;
using Core.Api.SwapModels;
using Core.Business.Interfaces;
using Core.Business.Models.DTO.SwapDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tah")]
    public class TreasuryAccountHolderController : MainController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDapperSwapDbRepository _dapperSwapDbRepository;

        public TreasuryAccountHolderController(IHttpClientFactory httpClientFactory,
                                               IDapperSwapDbRepository dapperSwapDbRepository, 
                                               INotifier notification,
                                               IUser appUser) : base(notification, appUser)
        {
            _httpClientFactory = httpClientFactory;
            _dapperSwapDbRepository = dapperSwapDbRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TreasuryAccountHolderRequest tah)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var client = _httpClientFactory.CreateClient("Alpha");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(tah), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("treasury_account_holders", content);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, "Something went wrong! Error Occured");
            }

            TreasuryAccountHolderResponse treasuryAccountHolderResponse = JsonConvert.DeserializeObject<TreasuryAccountHolderResponse>(response.Content.ReadAsStringAsync().Result);

            var tahModel = new TreasuryAccountHolderDTO
            {
                balance_account_id = tah.balance_account_id,
                currency = tah.currency,
                document = tah.document,
                legal_name = tah.legal_name,
                email = tah.email,
                phone_number = tah.phone_number,
                added_time = treasuryAccountHolderResponse.treasury_account_holder.added_time,
                id = Guid.Parse(treasuryAccountHolderResponse.treasury_account_holder.id),
                status = treasuryAccountHolderResponse.treasury_account_holder.status
            };

            var taMopdel = new TreasuryAccountDTO
            {
                account_type = treasuryAccountHolderResponse.treasury_accounts[0].account_type,
                added_time = treasuryAccountHolderResponse.treasury_accounts[0].added_time,
                currency = treasuryAccountHolderResponse.treasury_accounts[0].currency,
                id = treasuryAccountHolderResponse.treasury_accounts[0].id,
                status = treasuryAccountHolderResponse.treasury_accounts[0].status,
                treasury_account_holder_id = Guid.Parse(treasuryAccountHolderResponse.treasury_account_holder.id)
            };

            await _dapperSwapDbRepository.AddTreasuryAccountHolderAsync(tahModel);
            await _dapperSwapDbRepository.AddTreasuryAccountAsync(taMopdel);

            return Ok(treasuryAccountHolderResponse);
        }
    }
}