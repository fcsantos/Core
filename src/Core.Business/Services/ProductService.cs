using System;
using System.Threading.Tasks;
using Core.Business.Intefaces;
using Core.Business.Models;
using Core.Business.Models.Validations;

namespace Core.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUser _user;
         
        public ProductService(IProductRepository productRepository,
                              INotifier notifier,
                              IUser user) : base(notifier)
        {
            _productRepository = productRepository;
            _user = user;
        }

        public async Task Create(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Create(AuditColumns<Product>(product, "Create", _user.GetUserId()));
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(AuditColumns<Product>(product, "Update", _user.GetUserId()));
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}