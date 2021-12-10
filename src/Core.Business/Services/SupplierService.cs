using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Business.Interfaces;
using Core.Business.Models;
using Core.Business.Models.Validations;

namespace Core.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUser _user;

        public SupplierService(ISupplierRepository supplierRepository, 
                               IAddressRepository addressRepository,
                               INotifier notifier,
                               IUser user) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
            _user = user;
        }

        public async Task Create(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

            if (_supplierRepository.Search(f => f.Document == supplier.Document).Result.Any())
            {
                Notification("Já existe um fornecedor com este documento infomado.");
                return;
            }
            
            await _supplierRepository.Create(AuditColumns<Supplier, Address>(supplier, "Create",_user.GetUserId(), supplier.Address));
        }

        public async Task Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

            if (_supplierRepository.Search(f => f.Document == supplier.Document && f.Id != supplier.Id).Result.Any())
            {
                Notification("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _supplierRepository.Update(AuditColumns<Supplier, Address>(supplier, "Update", _user.GetUserId(), supplier.Address));
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }

        public async Task Delete(Guid id)
        {
            if (_supplierRepository.GetSupplierProductsAddress(id).Result.Products.Any()) 
            {
                Notification("O fornecedor possui produtos cadastrados!");
                return;
            }

            var endereco = await _addressRepository.GetAddressBySupplier(id);

            await _supplierRepository.Delete(id);

            if (endereco != null)
            {
                await _addressRepository.Delete(endereco.Id);
            }
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}