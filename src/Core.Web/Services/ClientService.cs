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
    public interface IClientService
    {
        Task<IEnumerable<ClientViewModel>> GetAll();

        Task<ClientViewModel> GetById(Guid id);

        Task<ClientViewModel> GetByUserId(Guid id);

        Task<ResponseResult> Create(ClientViewModel clientViewModel);

        Task<ResponseResult> Update(ClientViewModel clientViewModel);

        Task<ResponseResult> Delete(Guid id);

        Task<ResponseResult> DeleteUser(string id);
    }

    public class ClientService : Service, IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.APICoreUrl);
        }

        public async Task<IEnumerable<ClientViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/v1/clients");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlingErrorsResponse(response);

            return await DeserializedObjectResponse<IEnumerable<ClientViewModel>>(response);
        }

        public async Task<ClientViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/clients/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlingErrorsResponse(response);

            return await DeserializedObjectResponse<ClientViewModel>(response);
        }

        public async Task<ClientViewModel> GetByUserId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/clients/get-by-userid/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlingErrorsResponse(response);

            return await DeserializedObjectResponse<ClientViewModel>(response);
        }

        public async Task<ResponseResult> Create(ClientViewModel clientViewModel)
        {
            var clientContent = GetContent(clientViewModel);

            var response = await _httpClient.PostAsync("/api/v1/clients", clientContent);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!HandlingErrorsResponse(response)) return await DeserializedObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> Update(ClientViewModel clientViewModel)
        {
            var clientContent = GetContent(clientViewModel);

            var id = clientViewModel.Id;

            var response = await _httpClient.PutAsync($"/api/v1/clients/{id}", clientContent);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!HandlingErrorsResponse(response)) return await DeserializedObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }


        public async Task<ResponseResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/clients/{id}");

            if (!HandlingErrorsResponse(response)) return await DeserializedObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> DeleteUser(string id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");

            if (!HandlingErrorsResponse(response)) return await DeserializedObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
    }
}