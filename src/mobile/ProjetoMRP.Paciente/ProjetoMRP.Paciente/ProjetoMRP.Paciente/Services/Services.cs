using ProjetoMRP.Paciente.ViewModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ProjetoMRP.Paciente.Extensions;

namespace ProjetoMRP.Paciente.Services
{
    public class Services
    {
        protected static StringContent GetContent(object data)
        {
            var json =  new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            
            return json;
        }

        protected static async Task<T> DeserializedObjectResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool HandlingErrorsResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseResult ReturnOk()
        {
            return new ResponseResult();
        }
    }
}
