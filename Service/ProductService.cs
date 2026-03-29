using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared.ProductsDto;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<int> AddProductAsync(CreateProductDto dto)
        {
            var mappedProduct = _mapper.Map<CreateProductDto,Product>(dto);
            await _unitOfWork.GetRepository<Product,int>().AddAsync(mappedProduct);
            var result = await _unitOfWork.SaveChangesAsync();
            if ( result > 0 )
                return 1;
            return 0;
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetById(Id);
            if ( product is null )
                return false;
            _unitOfWork.GetRepository<Product,int>().Remove(product);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specs = new ProductSpecifications(queryParams);
            var products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync(specs);
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

        public async Task<int> UpdateProduct(int Id,UpdateProductDto dto)
        {
            var mappedProduct = _mapper.Map<UpdateProductDto,Product>(dto);
            mappedProduct.Id = Id;
            _unitOfWork.GetRepository<Product,int>().Update(mappedProduct);
            var Updated = await _unitOfWork.SaveChangesAsync();
            if ( Updated > 0 )
                return 1;
            return 0;
        }
    }
}
