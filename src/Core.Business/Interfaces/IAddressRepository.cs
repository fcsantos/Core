using Core.Business.Models;
using System;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressBySupplier(Guid supplierId); 
    }
}