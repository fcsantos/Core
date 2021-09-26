using Core.Business.Intefaces;
using Core.Business.Models;
using Core.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Business.Services
{
    public class ClientService : BaseService, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUser _user;

        public ClientService(IClientRepository clientRepository,
            INotifier notifier, IUser user) : base(notifier)
        {
            _clientRepository = clientRepository;
            _user = user;
        }

        public async Task Create(Client client)
        {
            if (!ExecuteValidation(new ClientValidation(), client)) return;

            if (_clientRepository.Search(c => c.Name == client.Name && c.NIF == client.NIF).Result.Any())
            {
                Notification("Já existe um Cliente com este NIF e Nome infomado.");
                return;
            }

            await _clientRepository.Create(AuditColumns<Client>(client, "Create", _user.GetUserId()));
        }

        public async Task Update(Client client)
        {
            if (!ExecuteValidation(new ClientValidation(), client)) return;

            if (_clientRepository.Search(c => c.Name == client.Name && c.NIF == client.NIF && c.Id != client.Id).Result.Any())
            {
                Notification("Já existe um Cliente com este NIF e Nome infomado.");
                return;
            }

            await _clientRepository.Update(AuditColumns<Client>(client, "Update", _user.GetUserId()));
        }

        public async Task Delete(Guid id)
        {
            await _clientRepository.Delete(id);
        }

        public async Task Delete(Client client)
        {
            client.IsActive = client.IsActive.Value ? false : true;
            await _clientRepository.Update(AuditColumns<Client>(client, "Update", _user.GetUserId()));
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }
    }
}