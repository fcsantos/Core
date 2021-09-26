using AutoMapper;
using Core.Api.Extensions;
using Core.Api.ViewModels;
using Core.Business.Intefaces;
using Core.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clients")]
    public class ClientsController : MainController
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository clientRepository,
                                 IClientService clientService,
                                 IMapper mapper,
                                 INotifier notifier, IUser user) : base(notifier, user)
        {
            _clientRepository = clientRepository;
            _clientService = clientService;
            _mapper = mapper;
        }

        [ClaimsAuthorize("Client", "Get")]
        [HttpGet]
        public async Task<IEnumerable<ClientViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ClientViewModel>>(await _clientRepository.GetAll());
        }

        [ClaimsAuthorize("Client", "Get")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClientViewModel>> GetById(Guid id)
        {
            var clientViewModel = _mapper.Map<ClientViewModel>(await _clientRepository.GetById(id));

            if (clientViewModel == null) return NotFound();

            return clientViewModel;
        }

        [ClaimsAuthorize("Client", "GetClient")]
        [HttpGet("get-by-userid/{id:guid}")]
        public async Task<ActionResult<ClientViewModel>> GetByUserId(Guid id)
        {
            var clientViewModel = _mapper.Map<ClientViewModel>(await _clientRepository.GetClientByUserId(id.ToString()));

            if (clientViewModel == null) return NotFound();

            return clientViewModel;
        }

        [ClaimsAuthorize("Client", "Create")]
        [HttpPost]
        public async Task<ActionResult<ClientViewModel>> Create(ClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clientService.Create(_mapper.Map<Client>(clientViewModel));

            return CustomResponse(clientViewModel);
        }

        [ClaimsAuthorize("Client", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClientViewModel>> Update(Guid id, ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.Id)
            {
                NotifyError("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(clientViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clientService.Update(_mapper.Map<Client>(clientViewModel));

            return CustomResponse(clientViewModel);
        }

        [ClaimsAuthorize("Client", "Delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClientViewModel>> Delete(Guid id)
        {
            var clientViewModel = await _clientRepository.GetById(id);

            if (clientViewModel == null) return NotFound();

            await _clientService.Delete(_mapper.Map<Client>(clientViewModel));

            return CustomResponse(clientViewModel);
        }
    }
}
