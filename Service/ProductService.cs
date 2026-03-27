using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using ServiceAbstraction;
using Shared.ProductsDto;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task AddProductAsync(ProductDto dto)
        {
            var mappedProduct = _mapper.Map<ProductDto,Product>(dto);
            await _unitOfWork.GetRepository<Product,int>().AddAsync(mappedProduct);
        }

        public async Task DeleteProduct(int Id)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetById(Id);
            if ( product is null )
                return;
            _unitOfWork.GetRepository<Product,int>().Remove(product);
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            var mappedProducts = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products);
            return mappedProducts;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetById(Id);
            if ( product is null )
                return null;
            var mappedProduct = _mapper.Map<Product,ProductDto>(product);
            return mappedProduct;
        }

        public void UpdateProduct(ProductDto dto)
        {
            var mappedProduct = _mapper.Map<ProductDto,Product>(dto);
            _unitOfWork.GetRepository<Product,int>().Update(mappedProduct);
        }
    }
}
