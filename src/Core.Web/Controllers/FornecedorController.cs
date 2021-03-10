using Core.Web.Extensions;
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

        [ClaimsAuthorize("Fornecedor", "Obter")]
        public async Task<IActionResult> Index()
        {
            return View(await _fornecedorService.Obter());
        }
    }
}