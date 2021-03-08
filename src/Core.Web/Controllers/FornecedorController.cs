using Core.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Web.Controllers
{
    public class FornecedorController : MainController
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [Route("fornecedor")]
        public async Task<IActionResult> Index()
        {
            var fornecedores = await _fornecedorService.Obter();

            return View();
        }
    }
}