using System;
using Core.Web.Extensions;
using Core.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Web.Models;
using System.Linq;

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


        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [HttpPost]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedor)
        {
            var response = await _fornecedorService.Adicionar(fornecedor);

            if (ResponsePossuiErros(response)) TempData["Erros"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();


            return RedirectToAction("Index", "Fornecedor");
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewData["Fornecedor"] = _fornecedorService.Obter(id).Result.Nome;

            return View(await _fornecedorService.Obter(id));
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPost]
        public async Task<IActionResult> EditPost(FornecedorViewModel fornecedor)
        {
            var response = await _fornecedorService.Atualizar(fornecedor);

            if (ResponsePossuiErros(response)) TempData["Erros"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();


            return RedirectToAction("Index", "Fornecedor");
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpPost]
        public ActionResult Delete(Guid fornecedorId)
        {
            var resposta = _fornecedorService.Deletar(fornecedorId).Result;

            if (ResponsePossuiErros(resposta)) return View("Index", _fornecedorService.Obter().Result);

            return RedirectToAction("Index", "Fornecedor");
        }
    }
}