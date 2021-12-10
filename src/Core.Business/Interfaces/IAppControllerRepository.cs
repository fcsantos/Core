using System;
using System.Threading.Tasks;
using Core.Business.Models;

namespace Core.Business.Interfaces
{ 
    public interface IAppControllerRepository : IRepository<AppController>
    {

        Task<AppController> GetControllerActions(Guid id);

    }
}