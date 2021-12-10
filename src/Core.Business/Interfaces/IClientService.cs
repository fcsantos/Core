using Core.Business.Models;
using System;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IClientService
    {
        Task Create(Client client);
        Task Update(Client client);
        Task Delete(Guid id);
        Task Delete(Client client);
    }
}