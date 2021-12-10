using System;
using System.Threading.Tasks;
using Core.Business.Models;

namespace Core.Business.Interfaces
{ 
    public interface ISupplierService : IDisposable
    {
        Task Create(Supplier supplier);
        Task Update(Supplier supplier);
        Task Delete(Guid id);

        Task UpdateAddress(Address address);
    }
}