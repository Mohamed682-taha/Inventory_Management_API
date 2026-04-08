using AutoMapper;
using CsvHelper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.ProductsDto;
using System.Globalization;

namespace Service
{
    class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
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

        public async Task<int> ImportCsv(IFormFile file)
        {
            using var stream = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(stream,CultureInfo.InvariantCulture);

            var mappedProducts = csv.GetRecords<CreateProductDto>().ToList();
            var products = _mapper.Map<List<CreateProductDto>,List<Product>>(mappedProducts);
            await _unitOfWork.GetRepository<Product,int>().AddRangeAsync(products);
            var result = await _unitOfWork.SaveChangesAsync();
            if ( result == 0 )
                throw new BadRequestException(["Failed to add products"]);
            return result;
        }

        public async Task<byte[]> ExportToCsv()
        {
            var specs = new ProductSpecifications();
            var products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync(specs);
            var mappedProducts = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductsExportDto>>(products);
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);
            using var csv = new CsvWriter(writer,CultureInfo.InvariantCulture);
            csv.WriteHeader<ProductsExportDto>();
            csv.NextRecord();
            foreach ( var product in mappedProducts )
            {
                csv.WriteRecord(product);
                csv.NextRecord();
            }
            writer.Flush();
            return memoryStream.ToArray();
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(Id);
            if ( product is null )
                return false;
            _unitOfWork.GetRepository<Product,int>().Remove(product);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product,int>();
            var Specs = new ProductSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(Specs);
            var CountSpecs = new ProductCountSpecification(queryParams);
            var Count = await Repo.CountAysnc(CountSpecs);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(Products);
            return new PaginatedResult<ProductDto>(queryParams.PageSize,queryParams.PageIndex,Count,MappedProducts);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var specs = new ProductSpecifications(Id);
            var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(specs);
            if ( product is null )
                return null;
            var mappedProduct = _mapper.Map<Product,ProductDto>(product);
            return mappedProduct;
        }

        public async Task<int> UpdateProductAsync(int Id,UpdateProductDto dto)
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
