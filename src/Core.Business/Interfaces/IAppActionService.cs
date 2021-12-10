using System;
using System.Threading.Tasks;
using Core.Business.Models;

namespace Core.Business.Interfaces
{
    public interface IAppActionService : IDisposable
    {
        Task Create(AppAction action);
        Task Update(AppAction action);
        Task Delete(Guid id);
    } 
}