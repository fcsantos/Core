using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Web.Extensions;
using Core.Web.Models;
using Core.Web.Resource;
using Core.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Core.Web.Controllers
{
    public class SuppliersController : MainController
    {
        private readonly ISupplierService _supplierService;
        private readonly IStringLocalizer<SuppliersController> _localizer;
        private readonly IStringLocalizer<Resources> _localizerGeneral;

        public SuppliersController(ISupplierService supplierService,
                                   IStringLocalizer<SuppliersController> localizer,
                                   IStringLocalizer<Resources> localizerGeneral)
        {
            _supplierService = supplierService;
            _localizerGeneral = localizerGeneral;
            _localizer = localizer;
        }

        [ClaimsAuthorize("Supplier", "Get")]
        public async Task<IActionResult> Index()
        {
            var teste = _localizer["teste"];
            var testeGeneral = _localizerGeneral["Bem vindo"];
            return View(await _supplierService.GetAll());
        }


        [ClaimsAuthorize("Supplier", "Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Supplier", "Create")]
        [HttpPost]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            var response = await _supplierService.Create(supplierViewModel);

            if (ResponseHasErrors(response)) TempData["Erros"] = 
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();


            return RedirectToAction("Index", "Suppliers");
        }

        [ClaimsAuthorize("Supplier", "Update")]
        [HttpGet("{id:guid}/EditSupplier")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await _supplierService.GetById(id);

            if (supplierViewModel == null) return NotFound();

            ViewData["EditSupplierName"] = _localizer["Editar Fornecedor {0}", supplierViewModel.Name];

            return View(supplierViewModel);
        }

        [ClaimsAuthorize("Supplier", "Update")]
        [HttpPost]
        public async Task<IActionResult> EditPost(SupplierViewModel supplierViewModel)
        {            
            var response = await _supplierService.Update(supplierViewModel);

            if (ResponseHasErrors(response)) TempData["Erros"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

            return RedirectToAction("Index", "Suppliers");
        }

        [ClaimsAuthorize("Supplier", "Delete")]
        [HttpPost]
        public ActionResult Delete(Guid Id)
        {
            var supplierViewModel = _supplierService.GetById(Id);

            if (supplierViewModel == null) return NotFound();

            var response = _supplierService.Delete(Id).Result;

            if (response.Errors.Messages.Any())           
                return Json(new DeleteResponseMessage { message = response.Errors.Messages.FirstOrDefault(), success = false });            

            return Json(new DeleteResponseMessage { message = _localizerGeneral["Registro deletado com sucesso"], success = true });
        }
    }
}