using System;
using System.Threading.Tasks;
using Core.Business.Models;

namespace Core.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    } 
}