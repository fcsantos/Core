using System;
using System.Threading.Tasks;
using Core.Business.Models;

namespace Core.Business.Interfaces
{ 
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierAddress(Guid id);
        Task<Supplier> GetSupplierProductsAddress(Guid id);
    }
}