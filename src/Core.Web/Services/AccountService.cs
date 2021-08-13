using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Web.Extensions;
using Core.Web.Models;
using Microsoft.Extensions.Options;

namespace Core.Web.Services
{
    public interface IAccountService
    {
        Task<UserResponseLogin> Register(UserRegister userRegister);

        Task<UserResponseLogin> RegisterUser(UserRegister userRegister);

        IEnumerable<RoleViewModel> GetRoles();
    }

    public class AccountService : Service, IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient,
                              IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(settings.Value.APICoreUrl);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registroContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("new-account", registroContent);

            if (!HandlingErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializedObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializedObjectResponse<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> RegisterUser(UserRegister userRegister)
        {
            var registroContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("new-account-user", registroContent);

            if (!HandlingErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializedObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializedObjectResponse<UserResponseLogin>(response);
        }

        public IEnumerable<RoleViewModel> GetRoles()
        {
            var response = _httpClient.GetAsync("roles/getAll").Result;

            HandlingErrorsResponse(response);

            return DeserializedObjectResponse<IEnumerable<RoleViewModel>>(response).Result;
        }
    }
}
