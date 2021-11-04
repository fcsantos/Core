using System.Collections.Generic;

namespace Core.Api.Extensions
{
    public class AppSettings
    {
        //quantas horas o token tem de validade
        public int ExpirationHours { get; set; }

        public string UrlResetPassword { get; set; }

        public string UrlLogin { get; set; }

        public string Url { get; set; }

        public Dictionary<string, string[]> ClaimsListClient { get; set; }

        public string RoleAdmin { get; set; }
        public string RoleClient { get; set; }

        public string DefaultPassword { get; set; }

        public string AutenticationJwksUrl { get; set; }
    }
}