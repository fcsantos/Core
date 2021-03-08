using Core.Web.Extensions;
using Core.Web.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public FornecedorService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        public async Task<IEnumerable<FornecedorViewModel>> Obter()
        {
            var response = await _httpClient.GetAsync("https://localhost:5001/api/v1/fornecedores");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<FornecedorViewModel>>(response);
        }
    }
}
