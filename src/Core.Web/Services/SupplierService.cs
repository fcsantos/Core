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
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierViewModel>> GetAll();

        Task<SupplierViewModel> GetById(Guid id);

        Task<ResponseResult> Create(SupplierViewModel supplierViewModel);

        Task<ResponseResult> Update(SupplierViewModel supplierViewModel);

        Task<ResponseResult> Delete(Guid id);
    }

    public class SupplierService : Service, ISupplierService
    {
        private readonly HttpClient _httpClient;

        public SupplierService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.APICoreUrl);
        }

        public async Task<IEnumerable<SupplierViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/v1/suppliers");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlingErrorsResponse(response);

            return await DeserializedObjectResponse<IEnumerable<SupplierViewModel>>(response);
        }

        public async Task<SupplierViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/suppliers/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlingErrorsResponse(response);

            return await DeserializedObjectResponse<SupplierViewModel>(response);
        }

        public async Task<ResponseResult> Create(SupplierViewModel supplierViewModel)
        {
            var supplierContent = GetContent(supplierViewModel);

            var response = await _httpClient.PostAsync("/api/v1/suppliers", supplierContent);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!HandlingErrorsResponse(response)) return await DeserializedObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> Update(SupplierViewModel supplierViewModel)
        {
            var supplierContent = GetContent(supplierViewModel);

            var id = supplierViewModel.Id;

            var response = await _httpClient.PutAsync($"/api/v1/suppliers/{id}", supplierContent);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!HandlingErrorsResponse(response)) return await DeserializedObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }


        public async Task<ResponseResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/suppliers/{id}");

            if (!HandlingErrorsResponse(response)) return await DeserializedObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
    }
}
