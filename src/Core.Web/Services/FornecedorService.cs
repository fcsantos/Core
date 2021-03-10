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

        Task<FornecedorViewModel> Obter(Guid id);

        Task<ResponseResult> Adicionar(FornecedorViewModel fornecedor);

        Task<ResponseResult> Atualizar(FornecedorViewModel fornecedor);

        Task<ResponseResult> Deletar(Guid id);
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

        public async Task<FornecedorViewModel> Obter(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/fornecedores/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<FornecedorViewModel>(response);
        }

        public async Task<ResponseResult> Adicionar(FornecedorViewModel fornecedor)
        {
            var fornecedorContent = ObterConteudo(fornecedor);

            var response = await _httpClient.PostAsync("/api/v1/fornecedores", fornecedorContent);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> Atualizar(FornecedorViewModel fornecedor)
        {
            var fornecedorContent = ObterConteudo(fornecedor);

            var id = fornecedor.Id;

            var response = await _httpClient.PutAsync($"/api/v1/fornecedores/{id}", fornecedorContent);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }


        public async Task<ResponseResult> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/fornecedores/{id}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
