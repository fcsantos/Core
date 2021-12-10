using System;
using System.Threading.Tasks;
using Core.Business.Interfaces;
using Core.Business.Models;
using Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(MyDbContext context) : base(context) { }

        public async Task<Address> GetAddressBySupplier(Guid fornecedorId) 
        {
            return await Db.Adresses.AsNoTracking().Include(f => f.Supplier)
                .FirstOrDefaultAsync(a => a.Supplier.Id == fornecedorId);
        }
    }
}