using Core.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Web.Services
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);

        Task RealizarLogin(UsuarioRespostaLogin resposta);

        Task Logout();

        IEnumerable<RoleViewModel> GetRoles();
    }
}