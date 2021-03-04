using System.Collections.Generic;

namespace Core.Api.Extensions
{
    public class AppSettings
    {
        //Chave de criptografia do token
        public string Secret { get; set; }
        
        //quantas horas o token tem de validade
        public int ExpiracaoHoras { get; set; }
        
        //quem emite: a app
        public string Emissor { get; set; }

        //em quais urls o token é válido
        public string ValidoEm { get; set; }
    }
}