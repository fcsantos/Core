using ProjetoMRP.Paciente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjetoMRP.Paciente.Services
{
    public class ApiService : Services
    {
        public const string UrlAPI = "https://mrpdev.lusodata.pt/api/api/v1/";

        public async Task Login(AuthenticationViewModel login)
        {
#if DEBUG
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
            };

#else
            var httpHandler = new HttpClientHandler();

#endif
            using (var client = new HttpClient(httpHandler))
            {

                var loginContent = GetContent(login);

                string urlComplete = UrlAPI + "login";
                var response = client.PostAsync(new Uri(urlComplete), loginContent).Result;

                var userResponse = await DeserializedObjectResponse<UserResponseLogin>(response);

                if (userResponse != null && !string.IsNullOrEmpty(userResponse.AccessToken))
                {
                    await SecureStorage.SetAsync("Token", userResponse.AccessToken);
                    await SecureStorage.SetAsync("UserID", userResponse.UserToken.Id);
					
					foreach (var claim in userResponse.UserToken.Claims)
                    {
                        if( claim.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")){
                            var n = claim.Value;
                            Application.Current.Properties["UserName"] = claim.Value;
                        }
                    }

                    Application.Current.Properties["accessToken"] = userResponse.AccessToken;
                    Application.Current.Properties["UserId"] = userResponse.UserToken.Id;
                    Application.Current.Properties["User"] = userResponse.UserToken;

                    Preferences.Set("accessToken", userResponse.AccessToken);
                    Preferences.Set("UserId", userResponse.UserToken.Id);
                }
            }

        }

        public static async Task<T> GetBy<T>(string endpoint)
        {

#if DEBUG
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
            };

#else
            var httpHandler = new HttpClientHandler();

#endif
            using (var client = new HttpClient(httpHandler))
            {
                var token = await SecureStorage.GetAsync("Token");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.BaseAddress = new Uri(UrlAPI);

                var url = string.Format("{0}", endpoint);

                var responseMessage = await client.GetAsync(url);

                return await DeserializedObjectResponse<T>(responseMessage);
            }
        }

        public static async Task<IEnumerable<T>> GetList<T>(string endpoint)
        {

#if DEBUG
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
            };

#else
            var httpHandler = new HttpClientHandler();

#endif
            try
            {

                using (var client = new HttpClient(httpHandler))
                {
                    var token = await SecureStorage.GetAsync("Token");
                    var userId = Guid.Parse(await SecureStorage.GetAsync("UserID"));

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    client.BaseAddress = new Uri(UrlAPI);

                    var url = string.Format("{0}", endpoint);

                    var responseMessage = await client.GetAsync(url);

                    if (responseMessage.StatusCode == HttpStatusCode.NotFound) return null;

                    return await DeserializedObjectResponse<IEnumerable<T>>(responseMessage);

                }

            }
            catch (Exception)
            {
                return Enumerable.Empty<T>();
            }
        }

        public static async Task<bool> Post<T>(string endpoint, T model)
        {
            try
            {
#if DEBUG
                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
                };

#else
            var httpHandler = new HttpClientHandler();

#endif
                using (var client = new HttpClient(httpHandler))
                {
                    var token = await SecureStorage.GetAsync("Token");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    client.BaseAddress = new Uri(UrlAPI);

                    var url = string.Format("{0}", endpoint);
                    Console.WriteLine("CHAMADA POST");
                    Console.WriteLine(url);
                    Console.WriteLine(model);
                    Console.WriteLine(GetContent(model).ReadAsStringAsync().Result);
                    HttpResponseMessage responseMessage = await client.PostAsync(url, GetContent(model));
                    return responseMessage.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }

        }

        public static async Task<bool> Put<T>(string endpoint, T model)
        {
            try
            {
#if DEBUG
                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
                };

#else
            var httpHandler = new HttpClientHandler();

#endif
                using (var client = new HttpClient(httpHandler))
                {
                    var token = await SecureStorage.GetAsync("Token");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    client.BaseAddress = new Uri(UrlAPI);

                    var url = string.Format("{0}", endpoint);

                    HttpResponseMessage responseMessage = await client.PutAsync(url, GetContent(model));
                    return responseMessage.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }

        }

    }

}


