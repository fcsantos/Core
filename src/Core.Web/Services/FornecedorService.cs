using Core.Web.Extensions;
using Core.Web.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Web.Services
{
    public interface IFornecedorService
    {
        Task<IEnumerable<FornecedorViewModel>> Obter();
    }

    public class FornecedorService : Service, IFornecedorService
    {
        private readonly HttpClient _httpClient;

        public FornecedorService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.APICoreUrl);
        }

        public async Task<IEnumerable<FornecedorViewModel>> Obter()
        {
            var response = await _httpClient.GetAsync("/api/v1/fornecedores");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<FornecedorViewModel>>(response);
        }
    }
}
